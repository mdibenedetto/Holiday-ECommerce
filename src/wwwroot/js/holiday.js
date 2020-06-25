document.addEventListener("DOMContentLoaded", () => {
    loadHolidays();
});

function loadHolidays() {
    const template = document.querySelector("template#holiday-item");
    const parent = document.querySelector("#holiday-container");

    holidayItems.forEach((item, index) => {
        var clone = template.content.cloneNode(true);

        clone.querySelector(".item-image").src = item.imageUrl;
        clone.querySelector(".item-title").textContent = item.title;
        clone.querySelector(".item-content").textContent = item.content;

        const links = clone.querySelectorAll(".item-link");
        links.forEach((link) =>
            link.setAttribute("href", "holiday-detail.html?id=" + (index + 1))
        );

        parent.appendChild(clone);
    });
}
