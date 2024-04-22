function enableElement(elementId) {
    let e = document.getElementById(elementId);

    if (e.hasAttribute("disabled")) {
        e.removeAttribute("disabled");
    }
}