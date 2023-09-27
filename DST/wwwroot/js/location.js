function getLocation()
{
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(sendToForm, showError);
    }
    else
    {
        alert("Geolocation is not supported by this browser.");
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

    //document.getElementById("#form-geolocation").submit();
    $("#form-geolocation").submit();
}

// Consider new function sendToText(position) -> "latitude, longitude"

function showError(error)
{
    switch (error.code)
    {
        case error.PERMISSION_DENIED:
            break;
        case error.POSITION_UNAVAILABLE:
            alert("Location information is unavailable.");
            break;
        case error.TIMEOUT:
            alert("The request to get user location timed out.");
            break;
        case error.UNKNOWN_ERROR:
        default:
            alert("An unknown error occurred.");
            break;
    }
}

function enableElement(elementId)
{
    let e = document.getElementById(elementId);

    if (e.hasAttribute("disabled"))
    {
        e.removeAttribute("disabled");
    }
}