//setTimeout(() => {
//    printJS({
//        printable: '/AutoClaveA/WritePrint',
//        type: 'pdf',
//        showModal: true,
//        modalMessage: "Document Loading...",
//        onError: (err) => console.log(err),
//        fallbackPrintable: () => console.log("FallbackPrintable"),
//        onPrintDialogClose: () => console.log('The print dialog was closed'),
//        onPrintDialogClose: printFinish
//    });
 

   

//}, 5000)

var getUrl = window.location;
var baseUrl ="";
//var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];

function printFinishA() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
   
//    //$.getJSON('/AutoClaveA/WritePrint', function (result) {
//    //    var toto = result;
//    //    alert(toto);
       
       
//    //});
  //  var myWindow = window.open('/AutoClaveA/WritePrint', "_blank"); 
    var myWindow = window.open(baseUrl+'/AutoClaveA/WritePrint'); 
    myWindow.focus();
    myWindow.print();
    window.location.href = baseUrl+'/AutoClaveA'
}


function printFinishB() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
     
    var myWindow = window.open(baseUrl+'/AutoClaveB/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveB'
}



function printFinishC() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveC/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveC'
}




function printFinishD() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveD/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveD'
}

function printFinishE() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveE/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href = baseUrl+'/AutoClaveE'
}

function printFinishF() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveF/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href = baseUrl+'/AutoClaveF'
}

function printFinishG() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveG/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveG'
}

function printFinishH() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveH/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveH'
}

function printFinishI() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveI/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href = baseUrl+'/AutoClaveI'
}

function printFinishJ() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveJ/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveJ'
}

function printFinishK() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveK/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href = baseUrl+'/AutoClaveK'
}


function printFinishL() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveL/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveL'
}

function printFinishM() {

    alert("El Archivo ha sido Impreso"); // What ever you want to do after window is closed .
    var myWindow = window.open(baseUrl+'/AutoClaveM/WritePrint');
    myWindow.focus();
    myWindow.print();
    window.location.href =baseUrl+ '/AutoClaveM'
}




