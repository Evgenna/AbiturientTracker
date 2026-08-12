document.addEventListener("click", (e) => {
    const tooltip = e.target.closest(".tooltip");

    // Закрыть все открытые подсказки
    document.querySelectorAll(".tooltip.show").forEach(t => {
        if (t !== tooltip) {
            t.classList.remove("show");
        }
    });

    if (tooltip) {
        e.stopPropagation();
        tooltip.classList.toggle("show");
    }
});