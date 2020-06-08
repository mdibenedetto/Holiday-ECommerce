const toolBar = `
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

const scriptLinks = `
<!-- CSS only -->
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">

<!-- JS, Popper.js, and jQuery -->
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
<link rel="stylesheet" href="./css/index.css" />
`;

document.head.insertAdjacentHTML("afterbegin", scriptLinks);
// body.insertAdjacentHTML("beforeend", footer);

document.addEventListener(
  "DOMContentLoaded",
  () => document.body.insertAdjacentHTML("afterbegin", toolBar),
  false
);

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
