function getLocation()
{
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
    else
    {
        alert("Geolocation is not supported by this browser.");
    }
}

// Consider using getElementByClassName()
// Rename this function to sendToForm(position)
function showPosition(position)
{
    document.getElementById("input-latitude").value = position.coords.latitude;
    document.getElementById("input-longitude").value = position.coords.longitude;
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