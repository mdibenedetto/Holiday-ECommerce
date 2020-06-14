const pageLoader = `<div class="loader-container">
  <div class="loader"></div>
</div>`;

let usetToolbar = ``;

window.currentUser = getCurrentUser();
if (window.currentUser) {
  // usetToolbar = `<li>Welcome ${window.currentUser.userName}</li>
  // <li><a href="login.html" onclick="logout()">Log out</a></li>`;

  usetToolbar = `    
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" 
            style = "width: auto;"
            href="#" 
            id="navbarDropdownMenuLink" 
            role="button" 
            data-toggle="dropdown"
            aria-haspopup="true" 
            aria-expanded="false">
          Welcome ${window.currentUser.userName}
        </a>
        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
          <a class="dropdown-item" href="user-account.html">Account</a>
          <a class="dropdown-item" href="orders.html" >Order history</a>
          <a class="dropdown-item"  href="cart.html">Cart</a>
          <a class="dropdown-item"  href="checkout-cart.html">Checkout</a>
          <a class="dropdown-item" href="login.html" onclick="logout()">
            Log out
          </a>
        </div>
      </li>
    `;
} else {
  // usetToolbar = `<li><a href="login.html">Login</a></li>
  // <li><a href="sign-up.html">Sign up</a></li>`;

  usetToolbar = `
    <li class="nav-item">
        <a class="nav-link" href="login.html">
          Login
        </a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="sign-up.html">
          Sign up
        </a>
      </li>`;
}

{
  /* <nav class="navbar nav-bar">
<ul>
  <li><a href="index.html">Home</a></li>
  <li><a href="holiday.html">Holidays</a></li>
  <li><a href="about.html">About</a></li>
  ${usetToolbar}
</ul>
</nav> */
}
const toolBar = `
   <header class="header-navbar container">
    <nav class="navbar-background navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="index.html">
          <span class="brand-text">DH</span>
        </a>
        <button class="navbar-toggler" 
            type="button" 
            data-toggle="collapse" 
            data-target="#navbarNavDropdown" 
            aria-controls="navbarNavDropdown" 
            aria-expanded="false" 
            aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavDropdown">
          <ul class="navbar-nav">
            <li class="nav-item"> 
              <a class="nav-link" href="index.html">
                Home <span class="sr-only">(current)</span>
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="holiday.html">
                Holidays
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="about.html">
                About
              </a>
            </li>
            </ul>
            
            <ul class="navbar-nav ml-auto">
            ${usetToolbar}  
            </ul>
          
        </div>
      </nav>    
    </header>
`;

function setActivePageLink(page) {
  const parseLink = (url) => {
    let index = url.lastIndexOf("/");
    let page = url.substr(index + 1);
    return page;
  };

  let activePage = parseLink(window.location.pathname);

  const links = document.querySelectorAll("nav li");
  links.forEach((li) => {
    const anchor = li.querySelector("a");
    const linkPage = parseLink(anchor.href);
    if (linkPage === activePage) {
      li.classList.add("active");
    }
  });
}

const cssLinks = `
<link rel="stylesheet" href="//netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.min.css">
<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
`;

const footer = `
<footer class="footer row ">
  <section class = "footer-body">
    © Copyright 2020 DreamHolidays - All Rights Reserved
  </section>
</footer>`;

// const scriptLinks = `
// <!-- CSS only -->
// <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">

// <!-- JS, Popper.js, and jQuery -->
// <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
// <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
// <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
// <link rel="stylesheet" href="./css/index.css" />
// `;

// document.head.insertAdjacentHTML("afterbegin", scriptLinks);
// body.insertAdjacentHTML("beforeend", footer);

document.addEventListener(
  "DOMContentLoaded",
  () => {
    initLayoutPage();
    setLinkListener();
    setActivePageLink();
  },
  false
);

function initLayoutPage() {
  document.head.insertAdjacentHTML("afterbegin", cssLinks);

  document.body.insertAdjacentHTML("afterbegin", pageLoader);
  document.body.insertAdjacentHTML("afterbegin", toolBar);
  document.body.insertAdjacentHTML("beforeend", footer);
  document.body.removeAttribute("data-loading");
}

function setLinkListener() {
  const links = document.querySelectorAll(
    "nav a:not(.navbar-brand):not([role=button])"
  );
  links.forEach((a) =>
    a.addEventListener("click", (e) => {
      document.body.setAttribute("data-loading", "true");
    })
  );
}

// TODO change when move to server side coding
function setCurrentUser(user) {
  localStorage.setItem("currentUser", JSON.stringify(user));
}
// TODO change when move to server side coding
function getCurrentUser() {
  const value = localStorage.getItem("currentUser");
  let currentUser = {};
  if (value) currentUser = JSON.parse(value);
  else currentUser = undefined;

  return currentUser;
}

// TODO change when move to server side coding
function logout() {
  localStorage.setItem("currentUser", null);
  // window.location = "/login.html";
}

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
