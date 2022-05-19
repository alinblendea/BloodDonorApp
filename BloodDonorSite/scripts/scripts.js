window.onload = function() {

    darkmode = document.getElementById("check1");
    page = document.getElementsByClassName("pagina")[0];
    page2 = document.getElementsByClassName("pagina")[1];
    nightText = document.getElementById("night");
    nightText2 = document.getElementById("night2");
    nightTitle = document.getElementById("nightTitle");
    nightTitle2 = document.getElementById("nightTitle2");

    function colors(color, colorPage) {
        document.body.style.background = color;
        if(page != null)
            page.style.background = colorPage;
        if(page2 != null)
            page2.style.background = colorPage;
    }
    darkmode.addEventListener("click", e => {
        if (darkmode.checked) {
            colors("#0c0d0e", "#3d3d3d")
            if(page != null)
                page.style.color = "white";
            if(page2 != null)
                page2.style.color = "white";
            if(nightText != null)
                nightText.style.color = "white";
            if(nightText2 != null)
                nightText2.style.color = "white";
            if(nightTitle != null)
                nightTitle.style.color = "white";
            if(nightTitle2 != null)
                    nightTitle2.style.color = "white";
        } else {
            colors("aliceblue", "#dddee7")
            if(page != null)
                page.style.color = "black";
            if(page2 != null)
                page2.style.color = "black";
            if(nightText != null)
                nightText.style.color = "#041664";
            if(nightText2 != null)
                nightText2.style.color = "#041664";
            if(nightTitle != null)
                nightTitle.style.color = "#041664";
            if(nightTitle2 != null)
                    nightTitle2.style.color = "#041664";
        }
    })
}