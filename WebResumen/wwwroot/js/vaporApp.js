(function (angular) {

    'use strict';
    angular.module('datatablesVaporApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
        controller('vaporCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter, $interval) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga


            $scope.dtOptions = DTOptionsBuilder.newOptions()
                .withOption('serverSide', false)
                .withOption('processing', false)
                .withOption('ajax', function (data, callback, settings) {


                    $http({
                        method: 'GET',
                        url: '/CiclosAutoClaveVapor/ListVapor',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function (result) {
                        $scope.searchid = result.data;
                        //console.log(result.data)
                        callback({

                            infox: result.data

                        });
                    });
                })

                .withDataProp('infox')

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
                            var iEndDateCol = 7;
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
                            var iEndDateColf = 8;
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
                        //text: "Execl <span class=\"loading_spinner_ex initial_hide\"><i class=\"fa fa-spinner fa-spin\"></i></span>",
                        className: "btn btn-md btn-success btn-track1",
                        title: localStorage.getItem("Fname") + "_Vapor_" + new Date().toLocaleString(),
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
                   // console.log(x)
                    var min = parseInt(x[0].trim());
                  //  console.log(min +"min")
                    var sec = (parseInt(x[1].trim())/60);
                  //  console.log(sec+"sec")
                    var s = (min + sec).toFixed(2);
                    return s
                }),
              //  DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2'),
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

                DTColumnBuilder.newColumn('tif3').withTitle('P.INICIAL F3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(5, 14)
                    return x
                }),

                DTColumnBuilder.newColumn('tif3').withTitle('TE2 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tif3').withTitle('TE3 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tif3').withTitle('TE4 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tff3').withTitle('P.FINAL F3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(6, 14)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE2 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE3 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE4 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE2 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE3 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE4 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('tminima').withTitle('TMinina Estr.'),
                DTColumnBuilder.newColumn('tmaxima').withTitle('TMaximaa Estr.'),

               // DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4'),
                DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5'),
                DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6'),
                DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF7a').withTitle('Duración F7A'),
                DTColumnBuilder.newColumn('duracionTotalF7a').withTitle('Duración F7A').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF8a').withTitle('Duración F8A'),
                DTColumnBuilder.newColumn('duracionTotalF8a').withTitle('Duración F8A').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF7b').withTitle('Duración F7B'),
                DTColumnBuilder.newColumn('duracionTotalF7b').withTitle('Duración F7B').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF8b').withTitle('Duración F8B'),
                DTColumnBuilder.newColumn('duracionTotalF8b').withTitle('Duración F8B').renderWith(function (data, type, full, meta) {
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
               // DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11'),
                DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),

                //DTColumnBuilder.newColumn('tif12').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                //    var x = data.substring(0, 7)
                //    return x
                //}),

                DTColumnBuilder.newColumn('tif12').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                    var Y = data.substring(0, 7)
                    var x = Y.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),

                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE2 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE3 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE4 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('difMaxMin').withTitle('FoMax-FoMin'),
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







})(angular);


