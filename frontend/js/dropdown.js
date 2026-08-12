document.querySelectorAll(".dropdown-header").forEach(header => {
    header.addEventListener("click", (e) => {
        e.stopPropagation();

        e.currentTarget.parentElement.classList.toggle("open");
    });
});