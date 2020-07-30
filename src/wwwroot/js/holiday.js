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
    destinations.toggleAttribute("data-disabled");
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
        });

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


    holidayItems.forEach(({ travelPackage, totalInCart }, index) => {
        const totalItems = (totalInCart > 0 ? ` (${totalInCart})` : '');

        var clone = template.content.cloneNode(true);
        clone.firstElementChild.setAttribute("data-id", travelPackage.id);

        clone.querySelector(".item-image").src = travelPackage.image;
        clone.querySelector(".item-title").textContent = travelPackage.name + totalItems;
        clone.querySelector(".item-content").textContent = travelPackage.description;

        const detailLinks = clone.querySelectorAll(".detail-link");
        detailLinks.forEach((link) =>
            link.setAttribute("href", "/holiday/detail?id=" + travelPackage.id)
        );

        const addTravelLink = clone.querySelector(".add-travel-link");

        if (addTravelLink) {
            addTravelLink.setAttribute("href", `/holiday/addtocart?tpId=${travelPackage.id}`);
            addTravelLink.setAttribute("data-id", travelPackage.id);
            addTravelLink.addEventListener("click", addTravelToCart)
        }

        parent.appendChild(clone);
    });
}

function addTravelToCart(e) {
    e.preventDefault();

    const errorMessage = () => alert("The service is not available right now.\nPlease try later.");

    fetch(
        "/api/addtocart?tpId=" + this.dataset.id,
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(res => {
            if (res.status != 200) {
                return errorMessage();
            }
            return res.json();
        })
        .then(res => {
            const { cart } = res;
            const { travelPackage } = cart;
            const blockTitle = document.querySelector(`[data-id="${travelPackage.id}"] .item-title`);

            blockTitle.innerText = travelPackage.name + ` (${cart.qty})`
        })
        .then(() => reloadCart())
        .catch(() => errorMessage())
}

function reloadCart() {
 
    fetch("/api/getcartsummary").then(res => {
        return res.text();
    })
        .then(
            html => {
                const cartBlock = document.querySelector("#cart");
                cartBlock.innerHTML = html;
            }
        )

}