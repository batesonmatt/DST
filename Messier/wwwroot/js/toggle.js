function toggle()
{
    var elements = document.getElementsByClassName("control-toggle");

    for (var i = 0; i < elements.length; i++)
    {
        if (elements[i].checked == true)
        {
            elements[i].setAttribute("value", "true");
        }
        else
        {
            elements[i].setAttribute("value", "false");
        }
    }
}