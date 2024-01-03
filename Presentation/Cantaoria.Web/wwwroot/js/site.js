// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    // make all currently active items inactive
    // (you can delete this block if you know that there are no active items when loading the page)
    document.querySelectorAll("a.nav-link.active").forEach(li => {
        li.classList.remove("active");
        li.attributes.removeNamedItem("aria-current");
    });

    // find the link to the current page and make it active
    document.querySelectorAll('.nav-link').forEach(a => {
        const href = a.getAttribute('href');

        // Eğer location.pathname, href ile başlıyorsa veya tamamen eşleşiyorsa
        if (location.pathname === href) {
            a.parentElement.classList.add('active');
            a.setAttribute('aria-current', 'page');
        } else {
            // Eğer aktif sayfa değilse, class ve aria-current attribute'u kaldırılır (opsiyonel)
            a.parentElement.classList.remove('active');
            a.removeAttribute('aria-current');
        }
    });

   
});

