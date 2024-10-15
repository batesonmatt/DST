function enableElement(elementId) {
    let e = document.getElementById(elementId);

    if (e.hasAttribute("disabled")) {
        e.removeAttribute("disabled");
    }
}

function focusElement(elementId) {
    setTimeout(() => { document.getElementById(elementId).focus() }, 1000);
}