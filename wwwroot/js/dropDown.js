document.getElementById("userDropdown").addEventListener("click", function (event) {
    var dropdownMenu = event.target.nextElementSibling;
    dropdownMenu.classList.toggle("show"); // Dropdown menüyü açıp kapatır
});
