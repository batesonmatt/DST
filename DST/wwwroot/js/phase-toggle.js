function phaseTrackOnce()
{
    let t = document.getElementById("input-phase-single");
    let e = document.getElementById("input-phase-cycles");

    if (t.checked === true)
    {
        e.value = 0;
        e.setAttribute("disabled", "disabled");
    }
    else
    {
        if (e.hasAttribute("disabled"))
        {
            e.removeAttribute("disabled");
        }
    }
}