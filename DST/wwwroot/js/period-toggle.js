function updateFixedTrackingToggle() {
    let s = document.getElementById("select-period-timeunit").selectedIndex;
    let f = document.getElementById("input-period-fixed");
    let w = document.getElementById("input-fixed-warning");

    switch (s) {
        case 3: // Days
        case 4: // Weeks
        case 5: // Months
        case 6: // Years
            if (f.hasAttribute("disabled")) {
                f.removeAttribute("disabled");
                w.hidden = true;
            }
            break;
        default:
            f.checked = false;
            f.setAttribute("disabled", "disabled");
            w.hidden = false;
            break;
    }
}

function updateAggregateToggle() {
    let s = document.getElementById("select-period-timeunit").selectedIndex;
    let f = document.getElementById("input-period-fixed");
    let a = document.getElementById("input-period-aggregate");
    let w = document.getElementById("input-aggregate-warning");

    switch (s) {
        case 5: // Months
        case 6: // Years
            if (f.checked === true) {
                if (a.hasAttribute("disabled")) {
                    a.removeAttribute("disabled");
                    w.hidden = true;
                }
            }
            break;
        default:
            a.checked = true;
            a.setAttribute("disabled", "disabled");
            w.hidden = false;
            break;
    }
}