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

    const min = 0, max = holidayItems.length - 3;
    const randomIndex = Math.floor(Math.random() * max) + min;
    
    const bannerItems = holidayItems.slice(randomIndex, randomIndex + 3).map(h => h.travelPackage);

    const parent = document.querySelector(".card-holiday-container");
    const temp = document.querySelector("template#card-holiday-item");

    bannerItems.forEach((item, index) => {



        const clone = temp.content.cloneNode(true);
        clone.querySelector(".item-image").src = item.image;

        const link = clone.querySelector(".item-link");
        link.setAttribute("href", `/holiday/detail?id=${item.id}`);
        link.addEventListener("click", () =>
            document.body.setAttribute("data-loading", "true")
        );

        parent.appendChild(clone);
    });
}
