$(document).ready(function () {
        

    getroles = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Home/Getroles",
                type: "GET"
            },
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { editable: false, nullable: false },
                        nom: { validation: { required: true } },
                    }
                }
            },
        }
    });

    getdepartements = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Home/Getdepartements",
                type: "GET"
            },
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { editable: false, nullable: false },
                        nom: { validation: { required: true } },
                    }
                }
            }
        }
    });

   

    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Home/Getutilisateurs",
                type: "GET"
            },
            create: {
                url: "/Home/Createutilisateur",
                type: "POST"
            },
            update: {
                url: "/Home/Updateutilisateur",
                type: "POST"
            },
            destroy: {
                url: "/Home/Deleteutilisateur",
                type: "POST"
            },
            parameterMap: function (data, type) {
                    if (type == "create") {
                        data.departementId = data.departement;
                }
                    data.entryDate = kendo.toString(kendo.parseDate(data.entryDate), 'dd/MM/yyyy');
                    return data;
            }
        },
        pageSize: 20,//evenement
        schema: {
            model: {
                id: "id",
                fields: {
                    id: { editable: false, nullable: false },
                    nom: {},
                    age: {},
                    email: {},
                    departement: { validation: { required: true } },
                    entryDate: { validation: { required: true }},
                    listeRoles: {},
                    roles: {type:"string"},
                    departementId: {},
                }
            }
        }
    });


    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        toolbar:["create"],
        columns: [
            "id",
            { field: "nom"},
            { field: "age"},
            { field: "email" },
            { field: "departement" ,template:"#=departement.nom#",title:"departement",editor:dropdownEditor},
            { field: "entryDate", format: 'dd/MM/yyyy', template: "#=kendo.toString(kendo.parseDate(entryDate), 'dd/MM/yyyy')#" ,editor: datepickerEditor},
            { field: "listeRoles", template: "#= promptroles(listeRoles) #", editor: orgEditor },
            { field: "roles", template: "#=roles#", editor: orgEditor2 },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "inline",
        edit: function (e) {

            var container = e.container; 
            var bbsMultiSelect = container.find("[id=bindbystring]").data("kendoMultiSelect");
            if (bbsMultiSelect) {
                bbsMultiSelect.value(promptarray(e.model.roles));//a chaque edit initialise les valeurs

            }
        }

    });
});

function orgEditor(container, options) {
    $("<select multiple='multiple' data-bind='value :" + options.field +"'/>")
        .appendTo(container)
        .kendoMultiSelect({
            dataSource: getroles,
            dataTextField: "nom",
            dataValueField: "id",
            open: function (e) {
                //console.log(this.value());
                //console.log(e.sender);
                //console.log(e.sender._valueBeforeCascade);
                //this.value=promptroles2(this.value());
            },
        });
}

function orgEditor2(container, options) {
    $("<select id='bindbystring' multiple='multiple' data-bind='value :" + options.field + "'/>")
        .appendTo(container)
        .kendoMultiSelect({
            dataSource: getroles,
            dataTextField: "nom",
            dataValueField: "nom",
            value: [promptarray(options.model.roles)],//promptarray(options.model.roles),


            change: function (e) {
                options.model.roles = promptroles2(this.value());//envoie les valeurs directement au model
            },
        });
}

function dropdownEditor(container, options) {

    $("<select data-bind='value :"+options.field+"'/>")
        .appendTo(container)
        .kendoDropDownList({
            autoBind:true,
            dataSource: getdepartements,
            dataTextField: "nom",
            dataValueField: "id",
        });
}

function datepickerEditor(container, options) {

    $('<input type="text" data-text-field="' + options.field + '" data-value-field="' + options.field + '" data-bind="value:' + options.field + '" />')
        .appendTo(container)
        .kendoDatePicker({
            format: '{0:dd/MM/yyyy}',
            //culture: "fr-FR",
        }); 
}

function promptroles(listeroles) {
    let res = "";
    for (var i = 0; i < listeroles.length; i++) {
        if (i == 0) {
            res += listeroles[i].nom;
        }
        else {
            res += "," + listeroles[i].nom;
        }
    }
    return res;
}

function promptroles2(listeroles) {
    let res = "";
    //console.log(listeroles);
    for (var i = 0; i < listeroles.length; i++) {
        if (i == 0) {
            res += listeroles[i];
        }
        else {
            res += "," + listeroles[i];
        }
    }
    return res;
}

function promptarray(mot) {
    //console.log(mot);
    let res = "";
    var array = [];
    for (var i = 0; i < mot.length; i++) {
        if (mot[i] == ',')
        {
            array.push(res);
            res = "";
            continue;
        }
        if (i == mot.length - 1)
        {
            res += mot[i];
            array.push(res);
            res = "";
            continue;
        }
        res += mot[i];
    }
    //console.log(array);
    return array;
}
