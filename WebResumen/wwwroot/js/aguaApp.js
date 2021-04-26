(function (angular) {
  
        'use strict';
        angular.module('datatablesAguaApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
            controller('aguaCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter, $interval) {
                DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga

              
                
                $scope.dtOptions = DTOptionsBuilder.newOptions()
                    .withOption('deferRender', true)
                    .withOption('serverSide', false)
                    .withOption('processing', false)
                    .withOption('searching', true)
                   // .withOption('scrollCollapse', true)
                   // .withOption('scroller', true)

                   
                    .withOption('ajax', function (data, callback, settings) {


                        $http({
                            method: 'GET',
                            url: '/CiclosAutoClaveAgua/ListAgua',
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }).then(function (response) {
                            $scope.searchid = response.data;
                            //console.log(response.data)
                            callback({

                                info: response.data
                                
                            });
                           
                        });
                    })
                   
                    .withDataProp('info')

                    //$scope.dtOptions = DTOptionsBuilder
                    //    .fromFnPromise(function () {
                    //        var defer = $q.defer();
                    //      $http.get('/CiclosAutoClaveAgua/ListAgua').then(function (result) {

                    //            $scope.searchid = result.data

                    //          defer.resolve(result.data);
                    //            console.log(result.data)
                    //            console.log(DTOptionsBuilder)

                    //        });


                    //        return defer.promise;
                    //    })




                     // .withOption('searching', false)

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
                                var mini = new Date($('#min').val());
                                var maxi = new Date($('#max').val());

                                var filterstart = mini;
                                var filterend = maxi;
                                                                                       
                                var iStartDateCol = 7; //using column 7 in this instance
                                var iEndDateCol =7;
                                var tabledatestart = data[iStartDateCol] ;
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

                        $.fn.dataTable.ext.search.push(
                            function (settings, data, dataIndex) {
                              
                                var fi = $('#fi').val();
                                var fa = $('#fa').val();
                                var fin = new Date($('#fi').val());
                                var fan = new Date($('#fa').val());


                                var filterstartf = fin;
                                var filterendf = fan;
                                var iStartDateColf = 8; //using column 5 in this instance
                                var iEndDateColf =8;
                                var tabledatestartf = data[iStartDateColf];
                                var tabledateendf = data[iEndDateColf];
                             
                                if (fi === "" && fa === "") {
                                    return true;
                                }
                                else if ((moment(filterstartf).isSame(tabledatestartf) || moment(filterstartf).isBefore(tabledatestartf)) && fa === "") {
                                    return true;
                                }
                                else if ((moment(filterendf).isSame(tabledatestartf) || moment(filterendf).isAfter(tabledatestartf)) && fi === "") {
                                    return true;
                                }
                                else if ((moment(filterstartf).isSame(tabledatestartf) || moment(filterstartf).isBefore(tabledatestartf)) && (moment(filterendf).isSame(tabledateendf) || moment(filterendf).isAfter(tabledateendf))) {
                                    return true;
                                }
                                return false;
                                

                               
                            }
                        );


                        $(document).ready(function () {
                            var table = $('#example').DataTable();

                            // Event listener to the two range filtering inputs to redraw on input
                            $('#fi, #fa').change(function () {
                                //Swal.fire('Por Favor Espere');
                                Swal.showLoading();
                                table.draw();
                                //Swal.hideLoading();
                                Swal.close();
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
                            title: localStorage.getItem("Fname") + "_Agua_" + new Date().toLocaleString(),
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
                            title:  localStorage.getItem("Fname")+"      "+ new Date().toLocaleString(),
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
                        //    extend: 'columnsToggle',
                        //    text: '<i class="fa fa-files-o"></i> Copy',
                        //    titleAttr: 'Copy',
                        //    exportOptions: {
                        //        columns: ':visible'
                        //    },

                        //},


                    ]
                    )

                    .withOption('scrollX', 'true')
                    .withOption('scrollY', '380px')
                   
                   
             
               
                    .withOption('lengthMenu', [[10, 50, 100, 1000], [10, 50, 100, 1000]]);

               
               


                $scope.dtColumns = [
                    DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {

                        return meta.row + 1

                    }),

                    //DTColumnBuilder.newColumn('id').withTitle('id'),             
                    DTColumnBuilder.newColumn('idAutoclave').withTitle('idAutoclave'),
                    DTColumnBuilder.newColumn('notas').withTitle('N.Carro'),
                    DTColumnBuilder.newColumn('numeroCiclo').withTitle('N.Progresivo'),
                    DTColumnBuilder.newColumn('programa').withTitle('N.Programa'),
                    DTColumnBuilder.newColumn('codigoProducto').withTitle('Cod.Producto'),
                    DTColumnBuilder.newColumn('lote').withTitle('Lote'),
                    DTColumnBuilder.newColumn('horaInicio').withTitle('HoraInicio').renderWith(function (data, type, full, meta) {
                        var x = moment(data, "DD.MM.YYYY HH.mm.ss").toDate();
                        var y = moment(x).format("YYYY-MM-DD HH:mm:ss");
                        return y
                    }),
                    DTColumnBuilder.newColumn('horaFin').withTitle('HoraFin').renderWith(function (data, type, full, meta) {
                        var x = moment(data, "DD.MM.YYYY HH.mm.ss").toDate();
                        var y = moment(x).format("YYYY-MM-DD HH:mm:ss");
                        return y
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1'),
                    DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2'),
                    DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3'),
                    DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4'),
                    DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    //DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5'),
                    DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    DTColumnBuilder.newColumn('tff5').withTitle('P.FINAL F5').renderWith(function (data, type, full, meta) {
                        var x = data.substring(5, 14)
                        return x
                    }),

                   // DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6'),
                    DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    DTColumnBuilder.newColumn('tif6').withTitle('P.INICIAL F6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(5, 14)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tif6').withTitle('TE2 IF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(14, 21)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tif6').withTitle('TE3 IF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(21, 28)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tif6').withTitle('TE4 IF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(28, 35)
                        return x
                    }),

                    DTColumnBuilder.newColumn('tff6').withTitle('P.FINAL F6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(6, 14)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tff6').withTitle('TE2 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(14, 21)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tff6').withTitle('TE3 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(21, 28)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tff6').withTitle('TE4 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(28, 35)
                        return x
                    }),

                    DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE2 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(2, 9)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE3 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(9, 18)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE4 FF6').renderWith(function (data, type, full, meta) {
                        var x = data.substring(18, 27)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tminima').withTitle('TMinina Estr.'),
                    DTColumnBuilder.newColumn('tmaxima').withTitle('TMaximaa Estr.'),
                   // DTColumnBuilder.newColumn('duracionTotalF7').withTitle('Duración F7'),
                    DTColumnBuilder.newColumn('duracionTotalF7').withTitle('Duración F7').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    DTColumnBuilder.newColumn('tif7').withTitle('P.FINAL F7').renderWith(function (data, type, full, meta) {
                        var x = data.substring(6, 14)
                        return x
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF8').withTitle('Duración F8'),
                    DTColumnBuilder.newColumn('duracionTotalF8').withTitle('Duración F8').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9'),
                    DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10'),
                    DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    //DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11'),
                    DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                   // DTColumnBuilder.newColumn('duracionTotalF12').withTitle('Duración F12'),
                    DTColumnBuilder.newColumn('duracionTotalF12').withTitle('Duración F12').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),
                    //DTColumnBuilder.newColumn('tff13').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                    //    var x = data.substring(0, 7)
                    //    return x
                    //}),
                    DTColumnBuilder.newColumn('tff13').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                        var Y = data.substring(0, 7)
                        var x = Y.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),

                   // DTColumnBuilder.newColumn('tiempoCiclo').withTitle('T.CALCULADO'),
                    DTColumnBuilder.newColumn('tiempoCiclo').withTitle('T.CALCULADO').renderWith(function (data, type, full, meta) {
                        var x = data.split(':');
                        var min = parseInt(x[0].trim());
                        var sec = (parseInt(x[1].trim()) / 60);
                        var s = (min + sec).toFixed(2);
                        return s
                    }),


                    DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE2 FF13').renderWith(function (data, type, full, meta) {
                        var x = data.substring(2, 9)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE3 FF13').renderWith(function (data, type, full, meta) {
                        var x = data.substring(9, 18)
                        return x
                    }),
                    DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE4 FF13').renderWith(function (data, type, full, meta) {
                        var x = data.substring(18, 27)
                        return x
                    }),
                    DTColumnBuilder.newColumn('difMaxMin').withTitle('FoMax-FoMin'),

                    //DTColumnBuilder.newColumn('fechaRegistro').withTitle('Registro').renderWith(function (data, type, full, meta) {
                    //    var x = data.substring(4, 10).split("-").reverse().join("-").replace("-", "/").replace("-", "/") + data.substring(4, 2).split("-").reverse().join("-").replace("-", "/").replace("-", "/") 
                    //    return x
                    //}),
                   
                ];


               


                function stateChange(iColumn, bVisible) {
                   // console.log('The column', iColumn, ' has changed its status to', bVisible);
                }



               

                function filterGlobal() {
                    $('#example').DataTable().search(
                        $('#global_filter').val(),
                        $('#global_regex').prop('checked'),
                        $('#global_smart').prop('checked')
                    ).draw();
                }

                function filterColumn(i) {
                    $('#example').DataTable().column(i).search(
                        $('#col' + i + '_filter').val().split("-").reverse().join("-").replace("-", "/").replace("-", "/").substring(0, 6) + $('#col' + i + '_filter').val().split("-").reverse().join("-").replace("-", "/").replace("-", "/").substring(8, 10), // agregado
                        $('#col' + i + '_regex').prop('checked'),
                        $('#col' + i + '_smart').prop('checked')
                    ).draw();
                   // console.log($('#col' + i + '_filter').val().split("-").reverse().join("-").replace("-", "/").replace("-", "/").substring(0, 6) + $('#col' + i + '_filter').val().split("-").reverse().join("-").replace("-", "/").replace("-", "/").substring(8,10));
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


             
                //$(document).ready(function () {
                //    // Create DataTable
                //    var table = $('#example').DataTable({
                //        retrieve: true,
                //        paging: false,
                //        dom: 'Pfrtip',
                //    });

                //    // Create the chart with initial data
                //    var container = $(' <div/>').insertBefore(table.table().container());

                //    var chart = Highcharts.chart(container[0], {
                //        chart: {
                //            spacingBottom: 15,
                //            spacingTop: 10,
                //            spacingLeft: 10,
                //            spacingRight: 10,
                //            // Explicitly tell the width and height of a chart
                //            width: null,
                //            height: null,
                           
                //            type: 'pie',

                //        },
                //        title: {
                //            text: 'Contador de Registros por Autoclaves',
                //        },
                //        series: [
                //            {
                //                data: chartData(table),
                //            },
                //        ],
                //    });

                //    // On each draw, update the data in the chart
                //    table.on('draw', function () {
                //        chart.series[0].setData(chartData(table));
                //    });
                //});

                //function chartData(table) {
                //    var counts = {};

                //    // Count the number of entries for each position
                //    table
                //        .column(1, { search: 'applied' })
                //        .data()
                //        .each(function (val) {
                //            if (counts[val]) {
                //                counts[val] += 1;
                //            } else {
                //                counts[val] = 1;
                //            }
                //        });

                //    // And map it to the format highcharts uses
                //    return $.map(counts, function (val, key) {
                //        return {
                //            name: key,
                //            y: val,
                //        };
                //    });
                //}

             

               


            })


    

  
 

})(angular);





   //$scope.dtInstance = {};
                //$scope.dtInstance1 = {};




                //$scope.searchTable = function () {
                //    console.log($scope.dtInstance);
                //    var query = $scope.searchText2;
                //    // console.log(query);

                //    //console.log($scope.dtInstance.DataTable);
                //    var resultado2 = $scope.dtInstance.DataTable.search(query)
                //    var resultado = $scope.dtInstance.DataTable.search(query).draw();
                //    console.log(resultado2);


                //};


                //$scope.searchTable2 = function () {

                //    var query = $scope.searchText;
                //    // console.log(query);

                //    //console.log($scope.dtInstance.DataTable);
                //    var resultado2 = $scope.dtInstance.DataTable.search(query)
                //    var resultado = $scope.dtInstance.DataTable.search(query).draw();
                //    console.log(resultado2);


                //};




                //$scope.dtInstance = {};

                //$scope.dtIntanceCallback = function (instance) {
                //    $scope.dtInstance = instance;
                //}

                // You should be able to get the table instance


                //$scope.dtInstance = {};
                //$scope.dtIntanceCallback = function (instance) {
                //    $scope.dtInstance = instance;
                //    $scope.dtInstance.DataTable.search(query).draw()

                //}
                //console.log($scope.dtInstance);
                //$scope.dtRebind = function () {
                //    $scope.dtInstance.DataTable.draw()
                //}

                //$scope.dtInstance = {};
                //$scope.search = search;

                //function search(query, dtInstance) {
                //    $scope.dtInstance.DataTable.columns(2).search(query).draw()
                //}

                //$scope.dtInstance = {};
                //$scope.search = search;

                //function search(query, programa) {
                //    $scope.dtInstance.DataTable.columns(3).search(query).draw()
                //}







                //$scope.programa = null;
                //$scope.numeroCiclo = null;
                //$scope.postdata = function (programa, numeroCiclo) {

                //    var datas = {
                //        nCiclo: numeroCiclo,
                //        nPrograma: programa
                //    };
                //    console.log(datas);
                //    $scope.dtOptions = DTOptionsBuilder.newOptions()
                //        .withOption('serverSide', false)
                //        .withOption('ajax', function (data, callback, settings) {
                //            $http({
                //                method: 'POST',
                //                url: '/CiclosAutoClaveAgua/ListAgua',
                //                data: datas,
                //                headers: {
                //                    'Content-Type': 'application/json'
                //                }
                //            }).then(function (response) {
                //                $scope.searchid = response.data;
                //                console.log(response.data)
                //                callback({

                //                    info: response.data
                //                });
                //            });
                //        })
                //        .withDataProp('info') // IMPORTANT¹
                //$scope.dtOptions = DTOptionsBuilder
                //    .fromFnPromise(function () {
                //        var defer = $q.defer();
                //        $http({
                //            method: 'POST',
                //            url: '/CiclosAutoClaveAgua/ListAgua',
                //            data: data,
                //            headers: {
                //                'Content-Type': 'application/json'
                //            }
                //        }).then(function (response) {
                //            if (response.data)
                //                console.log(response.data);
                //            $scope.msg = "Post Data Submitted Successfully!";
                //            console.log($scope.msg);
                //            $scope.searchid = response.data
                //            defer.all(response.data);

                //        }, function (response) {

                //            $scope.msg = "Service not Exists";



                //        });

                //        return defer.promise;
                //    })



                //}
