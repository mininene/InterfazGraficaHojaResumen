(function (angular) {
    'use strict';
    angular.module('datatablesAudiTrailApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
        controller('audiTrailCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga

            

            $scope.dtOptions = DTOptionsBuilder
                .fromFnPromise(function () {
                    var defer = $q.defer();
                    $http.get('/AudiTrail/ListAudiTrail').then(function (result) {
                        defer.resolve(result.data);
                        $scope.searchid = result.data
                        
                        //console.log(result.data)

                           
                    });

                   
                    return defer.promise;



                })
                //.withOption('searching', false)

                // Active ColVis plugin
                .withColVis()
                .withColVisOption('buttonText', 'Mostrar / Ocultar Columnas' + '<i class="fa fa-angle-down"></i>')
                // Add a state change function
                .withColVisStateChange(stateChange)

                .withOption('initComplete', function () {


                    $.fn.dataTable.ext.search.push(
                        function (settings, data, dataIndex) {


                            var min = $('#min').val();
                            var max = $('#max').val();
                            console.log(min);
                            var mini = new Date($('#min').val());
                            var maxi = new Date($('#max').val());
                            console.log(mini);

                            var filterstart = min;
                            var filterend = max;

                            var iStartDateCol = 3; //using column 7 in this instance
                            var iEndDateCol = 3;
                            var tabledatestart = data[iStartDateCol];
                            var tabledateend = data[iEndDateCol];

                            if (min === "" && max === "") {
                                return true;
                            }
                            else if ((moment(filterstart).isSame(tabledatestart) || moment(filterstart).isBefore(tabledatestart)) && max === "") {
                                return true;
                            }
                            else if ((moment(filterend).isSame(tabledatestart) || moment(filterend).isAfter(tabledatestart)) && min === "") {
                                return true;
                            }
                            else if ((moment(filterstart).isSame(tabledatestart) || moment(filterstart).isBefore(tabledatestart)) && (moment(filterend).isSame(tabledateend) || moment(filterend).isAfter(tabledateend))) {
                                return true;
                            }
                            return false;
                            //if (
                            //    (filterstart == "" || filterend == "") || (moment(tabledatestart).isSameOrAfter(filterend) && filterend === "") ||
                            //    (moment(tabledatestart).isSameOrAfter(filterend) && moment(tabledatestart).isSameOrBefore(filterstart))
                            //   // (moment(tabledatestart).isBetween(filterstart, filterend, undefined, '[]'))
                            //) {
                            //    return true;
                            //}
                            //return false;
                        }
                    );


                    $(document).ready(function () {
                        var table = $('#example').DataTable();

                        // Event listener to the two range filtering inputs to redraw on input
                        $('#min, #max').change(function () {
                            //Swal.fire('Por Favor Espere');
                            Swal.showLoading();
                            table.draw();
                            //Swal.hideLoading();
                            Swal.close();
                        });



                    });

                    function filterGlobal() {
                        $('#example').DataTable().search(
                            $('#global_filter').val(),
                            $('#global_regex').prop('checked'),
                            $('#global_smart').prop('checked')
                        ).draw();
                    }

                    function filterColumn(i) {
                        $('#example').DataTable().column(i).search(
                            $('#col' + i + '_filter').val(),
                            $('#col' + i + '_regex').prop('checked'),
                            $('#col' + i + '_smart').prop('checked')
                        ).draw();
                    }

                    $(document).ready(function () {
                        $('#example').DataTable();

                        $('input.global_filter').on('keyup click', function () {
                            filterGlobal();
                        });

                        $('input.column_filter').on('keyup click', function () {
                            filterColumn($(this).parents('tr').attr('data-column'));
                        });
                    });
                  


                })

             
            .withPaginationType('full_numbers')
              
                .withLanguage({
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ",",
                    "sLengthMenu": "Mostrar   _MENU_   registros",
                    "sLoadingRecords": "Cargando...",
                    "sProcessing": "procesando...",
                    "sSearch": "Buscar:",
                    "sZeroRecords": "No hay registros encontrados",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                })

           
                .withButtons([
                    {
                        
                        extend: 'excel',
                        text: '<i class="fa fa-file-text-o"></i> Excel',
                        title: localStorage.getItem("Fname") + "_Auditrail_" + new Date().toLocaleString(),
                        titleAttr: 'Excel', action: function (e, dt, node, config) {
                            // show sweetalert ...
                            Swal.fire('Por Favor Espere');
                            Swal.showLoading();

                            setTimeout(function () {
                                $.fn.dataTable.ext.buttons.excelHtml5.action.call(this, e, dt, node, config);
                                Swal.fire('Descarga Completada');
                                Swal.hideLoading();
                            }, 50);
                        },
                        exportOptions: {
                            columns: ':visible'
                        },
                       
                    },
                    {
                        extend: 'print',
                        customize: function (win) {

                            var last = null;
                            var current = null;
                            var bod = [];

                            var css = '@page { size: landscape; }',
                                head = win.document.head || win.document.getElementsByTagName('head')[0],
                                style = win.document.createElement('style');

                            style.type = 'text/css';
                            style.media = 'print';

                            if (style.styleSheet) {
                                style.styleSheet.cssText = css;
                            }
                            else {
                                style.appendChild(win.document.createTextNode(css));
                            }

                            head.appendChild(style);
                        },
                        footer: 'true',
                        text: '<i class="fa fa-print" aria-hidden="true"></i> Imprimir',
                        title: localStorage.getItem("Fname") + "      " + new Date().toLocaleString(),
                        titleAttr: 'Print',
                        exportOptions: {
                            columns: ':visible'
                        },
                    },
                    {
                        extend: 'copy',
                        text: '<i class="fa fa-files-o"></i> Copiar',
                        titleAttr: 'Copy',
                        exportOptions: {
                            columns: ':visible'
                        },
                        
                    },
                    //{
                    //    extend: 'pdf',
                    //    text: '<i class="fa fa-print" aria-hidden="true"></i> Pdf',
                    //    titleAttr: 'PDF'

                    //},
                     //{
                        //extend: 'colvis',
                        //text: '<i class="fa fa-print" aria-hidden="true"></i> Columnas',
                        //titleAttr: 'Columnas'

                    //},
                 
                     
                ]
            )
               
                .withOption('scrollX', 'true')
                .withOption('scrollY', '380px')
               
                .withOption('lengthMenu', [[10, 50, 100, 1000], [10, 50, 100, 1000]]);
      
           
            $scope.formatDate = function (date) {
                var dateOut = new Date(date);
                dateOut.setMonth(dateOut.getMonth() - 1);
                return dateOut;
            };

             

            $scope.dtColumns = [
                DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {
               
                    return meta.row + 1
                   
                }),
                
                //DTColumnBuilder.newColumn('id').withTitle('id'),             
                DTColumnBuilder.newColumn('usuario').withTitle('Usuario'),
                DTColumnBuilder.newColumn('tabla').withTitle('Tabla'),

                DTColumnBuilder.newColumn('fechaHora').withTitle('FechaHora').renderWith(function (data, type, full, meta) {
                    var fechaHora = data;
                    console.log(fechaHora)
                    var date = new Date(fechaHora);
                    var dateStr =
                        date.getFullYear() + "-" +
                        ("00" + (date.getMonth() + 1)).slice(-2) + "-" +
                        ("00" + date.getDate()).slice(-2)+ " " +
                       
                       
                        ("00" + date.getHours()).slice(-2) + ":" +
                        ("00" + date.getMinutes()).slice(-2) + ":" +
                        ("00" + date.getSeconds()).slice(-2);
                    return (dateStr)
                }),

            
                DTColumnBuilder.newColumn('evento').withTitle('Evento'),

                DTColumnBuilder.newColumn('campo').withTitle('Campo'),
                DTColumnBuilder.newColumn('valor').withTitle('ValorAntiguo'),
                DTColumnBuilder.newColumn('valorActualizado').withTitle('ValorActualizado'),
                DTColumnBuilder.newColumn('comentario').withTitle('Motivo'),
               
            ]
            function stateChange(iColumn, bVisible) {
               // console.log('The column', iColumn, ' has changed its status to', bVisible);
            }


          







         
        }) 
 

})(angular);

