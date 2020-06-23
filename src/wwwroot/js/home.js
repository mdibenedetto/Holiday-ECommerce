document.addEventListener("DOMContentLoaded", () => {
    loadSlider();
    loadBanner();
});

function loadSlider() {
    const sliderBaseImage = `./img/slider`;
    const sliderItems = [
        {
            imageUrl: sliderBaseImage + "/slide1.jpg",
            title: "Maldives",
            price: 2000,
            content: "Nulla vitae elit libero, a pharetra augue mollis interdum.",
        },
        {
            imageUrl: sliderBaseImage + "/slide2.jpg",
            title: "Venice",
            price: 1600,
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
        },
        {
            imageUrl: sliderBaseImage + "/slide3.jpg",
            title: "London",
            price: 1000,
            content:
                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
        },
    ];

    const parent = document.querySelector(".carousel-inner");
    const temp = document.querySelector("template#carousel-item");

    sliderItems.forEach((item) => {
        const clone = temp.content.cloneNode(true);
        clone.querySelector(".item-image").src = item.imageUrl;
        clone.querySelector(".item-title").textContent = item.title;
        clone.querySelector(".item-content").textContent = item.content;
        parent.appendChild(clone);
    });
    parent.children[0].classList.add("active");
}

function loadBanner() {
    const bannerBaseImage = `./img/banner`;
    const bannerItems = [
        {
            id: 1,
            imageUrl: bannerBaseImage + "/ban1.jpg",
            title: "Barcelona",
            price: 1000,
        },
        {
            id: 5,
            imageUrl: bannerBaseImage + "/ban2.jpg",
            title: "Goa",
            price: 1500,
        },
        {
            id: 6,
            imageUrl: bannerBaseImage + "/ban3.jpg",
            title: "Paris",
            price: 1600,
        },
    ];

    const parent = document.querySelector(".card-holiday-container");
    const temp = document.querySelector("template#card-holiday-item");

    bannerItems.forEach((item) => {
        const clone = temp.content.cloneNode(true);
        clone.querySelector(".item-image").src = item.imageUrl;

        const link = clone.querySelector(".item-link");
        link.setAttribute("href", "holiday-detail.html?id=" + item.id);
        link.addEventListener("click", () =>
            document.body.setAttribute("data-loading", "true")
        );

        parent.appendChild(clone);
    });
}
