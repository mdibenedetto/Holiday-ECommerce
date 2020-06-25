window.currentUser = null;

const form = document.querySelector("form");

form.addEventListener("submit", (e) => {
  e.preventDefault();

  const userName = form.querySelector("[type=email]").value;

  // TODO change when move to server side coding
  setCurrentUser({
    userName,
  });
  //window.location = "/holiday";
});

ß