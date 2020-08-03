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

        // display Travel Package Link
        const detailLinks = clone.querySelectorAll(".detail-link");
        detailLinks.forEach((link) =>
            link.setAttribute("href", "/holiday/detail?id=" + travelPackage.id)
        );

        // add cart item link
        const addTravelLink = clone.querySelector(".add-travel-link");

        if (addTravelLink) {
            addTravelLink.setAttribute("href", `/holiday/addtocart?tpId=${travelPackage.id}`);
            addTravelLink.setAttribute("data-id", travelPackage.id);
            addTravelLink.addEventListener("click", e => updateTravelCart(e, true));
        }

        // remove-cart item link
        const removeTravelLink = clone.querySelector(".remove-travel-link");
        removeTravelLink.style.display = !totalItems ? 'none' : '';

        if (removeTravelLink) {
            addTravelLink.setAttribute("href", `/holiday/removetocart?tpId=${travelPackage.id}`);
            addTravelLink.setAttribute("data-id", travelPackage.id);
            addTravelLink.addEventListener("click", e => updateTravelCart(e, false));
        }

        parent.appendChild(clone);
    });
}


function updateTravelCart(e, isAdd = true) {
    e.preventDefault();

    const errorMessage = (err) => {
        alert("The service is not available right now.\nPlease try later.");
        console.error("Error: "  + err);
    };

    // url Params

    let ACTION = isAdd ? "addtocart" : "remofromcart";

    const { id } = e.target.dataset;

    fetch(
        `/api/${ACTION}?tpId=${id}`,
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(res => {
            if (res.status != 200) {
                return errorMessage("status code: " + status);
            }
            return res.json();
        })
        .then(res => {
            const { cart } = res;
            const { travelPackage } = cart;

            const card = document.querySelector(`[data-id="${travelPackage.id}"]`);

            // update title with quantity
            const blockTitle = card.querySelector(`.item-title`);
            blockTitle.innerText = travelPackage.name + ` (${cart.qty})`;

            // display remove link if quantity > 0;
            const removeTravelLink = card.querySelector(`.remove-travel-link`);
            removeTravelLink.style.display = (cart.qty == 0) ? 'none' : '';


        })
        .then(() => reloadCart())
        .catch((err) => {
            errorMessage(err);
          
        })
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