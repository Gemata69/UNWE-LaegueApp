document.addEventListener("DOMContentLoaded", function () {
    const mobileMenuToggle = document.querySelector(".mobile-menu-toggle");
    const navLinksWrapper = document.querySelector(".nav-links-wrapper");

    if (mobileMenuToggle && navLinksWrapper) {
        mobileMenuToggle.addEventListener("click", function (e) {
            e.preventDefault(); 
            console.log("Хамбургерът е натиснат!"); 

            navLinksWrapper.classList.toggle("active");

            const icon = mobileMenuToggle.querySelector("span");
            if (navLinksWrapper.classList.contains("active")) {
                icon.textContent = "✖";
            } else {
                icon.textContent = "☰";
            }
        });
    }
});