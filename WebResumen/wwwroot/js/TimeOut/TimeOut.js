$.getJSON('/Home/Tiempo', function (result) {
    var secondt = result;
    var second = secondt * 60;

    localStorage.setItem("Inactividad", second);
    var valor = localStorage.getItem("Inactividad") ;




    var IDLE_TIMEOUT = valor; //seconds
    var _idleSecondsTimer = null;
    var _idleSecondsCounter = 0;
    console.log(IDLE_TIMEOUT);
    document.onclick = function () {
        _idleSecondsCounter = 0;
    };

    document.onmousemove = function () {
        _idleSecondsCounter = 0;
    };

    document.onkeypress = function () {
        _idleSecondsCounter = 0;
    };

    _idleSecondsCounter = window.setInterval(CheckIdleTime, 1000);

    function CheckIdleTime() {
        _idleSecondsCounter++;
        var oPanel = document.getElementById("SecondsUntilExpire");
        if (oPanel)
            // oPanel.innerHTML = (IDLE_TIMEOUT - _idleSecondsCounter) + "";
            var seconds = IDLE_TIMEOUT - _idleSecondsCounter;
        console.log(seconds);

        var hour = Math.floor(seconds / 3600);
        hour = (hour < 10) ? '0' + hour : hour;
        var minute = Math.floor((seconds / 60) % 60);
        minute = (minute < 10) ? '0' + minute : minute;
        var seconds = seconds % 60;
        seconds = (seconds < 10) ? '0' + seconds : seconds;
        // return hour + ':' + minute + ':' + second;
        console.log(hour + ':' + minute + ':' + seconds);
        oPanel.innerHTML = (hour + ':' + minute + ':' + seconds) + "";



        if (_idleSecondsCounter >= IDLE_TIMEOUT) {
            window.clearInterval(_idleSecondsCounter);
            alert("Sesión Expirada!");

            window.location.href = "/Home/Logout"

            _idleSecondsCounter = 0;
            //document.location.href = "logout.html";
            // _idleSecondsCounter = 0;

        }
    }
});