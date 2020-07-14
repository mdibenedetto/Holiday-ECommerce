document.addEventListener("DOMContentLoaded", () => {
    loadHolidays(holidayItems);
});


const API_URL = "/api/travelpackages";

const chkAllDestination = document.querySelector("#chkAllDestination");
chkAllDestination.addEventListener("click", toggleDestinationList);

const destinations = document.querySelector("#destinations");

const priceRange = document.querySelector("#price-range");
priceRange.setAttribute("min", 0);
priceRange.setAttribute("max", 5000);
priceRange.setAttribute("step", 10);
priceRange.addEventListener("input", updateSearchPrice);

const btnSearch = document.querySelector("button#btnSearch");
btnSearch.addEventListener("click", filterHolidays);


function toggleDestinationList(e) {
    if (this.checked) {
        destinations.selectedIndex = -1;
    } 
}

function updateSearchPrice() {
    const price = document.querySelector("#price");
    price.innerHTML = "€ " + computedPrice();
}

function computedPrice() {
    return priceRange.value;
    //return Math.round(priceRange.value * 100);
}

function filterHolidays() {   
    document.body.setAttribute("data-loading", true);

    const price = priceRange.value;
    const destList = Array.prototype
        .map
        .call(destinations.selectedOptions, opt => "destinations=" + opt.value);

    let url = API_URL + "?void";

    if (price) {
        url += "&price=" + computedPrice();
    }

    if (!chkAllDestination.checked && destList && destList.length > 0) {
        url += "&" + destList.join("&");
    }
       
    debugger;
    fetch(url)
        .then(res => res.json())
        .then(data => {
            loadHolidays(data);
            document.body.removeAttribute("data-loading");
        })
        .catch(err => {
            document.body.removeAttribute("data-loading");
            console.error(err);
        }) ; 
    
}

function loadHolidays(holidayItems) {
    const template = document.querySelector("template#holiday-item");
    const parent = document.querySelector("#holiday-container");

    parent.innerHTML = "";

    if (!holidayItems || holidayItems.length == 0) {
        return parent.innerHTML =
            `
            <div class="alert-no-result alert alert-primary" role="alert">
                <h1> No packages found.Please use different searching params </h1>
            </div>
            `;     
     }   

    holidayItems.forEach((item, index) => {
        var clone = template.content.cloneNode(true);

        clone.querySelector(".item-image").src = item.image;
        clone.querySelector(".item-title").textContent = item.name;
        clone.querySelector(".item-content").textContent = item.description;

        const links = clone.querySelectorAll(".item-link");
        links.forEach((link) =>
            link.setAttribute("href", "/holiday/detail?id=" + item.id)
        );

        parent.appendChild(clone);
    });
}
