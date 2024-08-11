const options = {
    enableHighAccuracy: false,
    timeout: 5000,
    maximumAge: 0
}

function getLocation()
{
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(sendToForm, showGeolocationError, options);
    }
    else
    {
        alert("Location service is not available.");
    }
}

// Consider using getElementByClassName()
function sendToForm(position)
{
    let tz = Intl.DateTimeFormat().resolvedOptions().timeZone;
    document.getElementById("input-timezone").value = tz;

    document.getElementById("select-timezone").value = null;
    document.getElementById("select-timezone").setAttribute("disabled", "disabled");

    document.getElementById("input-latitude").value = position.coords.latitude;
    document.getElementById("input-longitude").value = position.coords.longitude;

    //document.getElementById("accuracy").innerHTML = position.coords.accuracy;

    //document.getElementById("input-timezone").value = tz;
    //document.getElementById("input-latitude").value = position.coords.latitude;
    //document.getElementById("input-longitude").value = position.coords.longitude;
    //document.getElementById("accuracy").value = position.coords.accuracy;

    document.getElementById("form-geolocation").submit();
    //$("#form-geolocation").submit();
}

function showGeolocationError(error)
{
    switch (error.code)
    {
        case error.PERMISSION_DENIED:
            alert("Location service is blocked for this site. Please allow permission to access this device's location to continue.");
            break;
        case error.POSITION_UNAVAILABLE:
            alert("Location service is unavailable.");
            break;
        case error.TIMEOUT:
            alert("The request timed out.");
            break;
        case error.UNKNOWN_ERROR:
        default:
            alert("An unknown error occurred.");
            break;
    }
}