const dummyBaseImage = `https://via.placeholder.com`;
const items = [
  {
    imageUrl: dummyBaseImage + "/1440x635.png/012542?text=Image-1",
    title: "First slide label",
    content: "Nulla vitae elit libero, a pharetra augue mollis interdum.",
  },
  {
    imageUrl: dummyBaseImage + "/1440x635.png/d8b1d3?text=Image-2",
    title: "Second slide label",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    imageUrl: dummyBaseImage + "/1440x635.png/144238144?text=Image-3",
    title: "Third slide label",
    content: "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
  },
];

document.addEventListener("DOMContentLoaded", () => {
  const carouselInner = document.querySelector(".carousel-inner");
  const temp = document.querySelector("template#carousel-item");
 
  items.forEach((item) => {
    const clone = temp.content.cloneNode(true);
    clone.querySelector(".item-image").src = item.imageUrl;
    clone.querySelector(".item-title").textContent = item.title;
    clone.querySelector(".item-content").textContent = item.content;
    carouselInner.appendChild(clone);
  });
  carouselInner.children[0].classList.add("active");
});
