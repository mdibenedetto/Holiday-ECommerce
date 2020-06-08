const {
  body,
  head,
} = document;

const toolBarar = `
   <header class="header-nav-bar">
      <nav class="nav-bar">
        <ul>
          <li><a href="index.html">Home</a></li>
          <li><a href="holiday.html">Holidays</a></li>
          <li><a href="about.html">About</a></li>
          <li><a href="login.html">Login</a></li>
          <li><a href="register.html">Register</a></li>
        </ul>
      </nav>
    </header>
`;
const footer = ``;

body.insertAdjacentHTML("afterbegin", toolBarar);
body.insertAdjacentHTML("beforeend", footer);

head.insertAdjacentHTML("afterbegin",
    `<link
        rel="stylesheet"
        href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"
        integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk"
        crossorigin="anonymous"
        />
        <link rel="stylesheet" href="./css/index.css" />
        `);

   
// const links = document.querySelectorAll("a");
// links.forEach((a) => a.addEventListener("click", (e) => e.preventDefault()));

// let currentIndex = 0;
// const images = document.querySelectorAll(".image-container");
// setInterval(() => {
//   images.forEach((image, index) => {
//     image.classList.remove("active");
//   });

//   if (currentIndex < images.length - 1) {
//     currentIndex++;
//   } else {
//     currentIndex = 0;
//   }

//   images[currentIndex].classList.add("active");
// }, 2000);
