document.addEventListener("DOMContentLoaded", () => {
    loadHolidays(holidayItems);
});

const API_URL = "/api/travelpackages";

// =====================================================
// destinations
// =====================================================
const ddDestinations = document.querySelector("#destinations");
const chkAllDestination = document.querySelector("#chkAllDestination");
chkAllDestination.addEventListener("click", (e) => toggleCategoryList(e, ddDestinations));

// =====================================================
// categories
// =====================================================
const ddCategories = document.querySelector("#categories");
const chkAllCategories = document.querySelector("#chkAllCategories");
chkAllCategories.addEventListener("click", (e) => toggleCategoryList(e, ddCategories));

// =====================================================
// destinations and categories
// =====================================================
function toggleCategoryList({ target }, dropdown) {
    if (this.checked) {
        dropdown.selectedIndex = -1;
    }
    dropdown.toggleAttribute("data-disabled");
}

// =====================================================

const priceRange = document.querySelector("#price-range");
priceRange.setAttribute("min", 0);
priceRange.setAttribute("max", 5000);
priceRange.setAttribute("step", 10);
priceRange.addEventListener("input", updateSearchPrice);

const btnSearch = document.querySelector("button#btnSearch");
btnSearch.addEventListener("click", filterHolidays);

function updateSearchPrice() {
    const price = document.querySelector("#price");
    price.innerHTML = "€ " + computedPrice();
}

function computedPrice() {
    return priceRange.value;
    //return Math.round(priceRange.value * 100);
}

function buildListFilter(dropdown, dropdownCheckbox) {
    let urlParam = "";

    if (!dropdownCheckbox.checked) {
        const dropdownValues = Array.prototype.map.call(
            dropdown.selectedOptions,
            (opt) => dropdown.id + "=" + opt.value
        );

        if (dropdownValues && dropdownValues.length > 0) {
            urlParam += "&" + dropdownValues.join("&");
        }
    }

    return urlParam;
}

function filterHolidays() {
    document.body.setAttribute("data-loading", true);

    let url = API_URL + "?void";
    // ==================================================
    // set query param: price
    // ==================================================
    const price = priceRange.value;
    if (price) {
        url += "&price=" + computedPrice();
    }
    // ==================================================
    // set query param: destinations
    // ==================================================

    url += buildListFilter(ddDestinations, chkAllDestination);
 
    // ==================================================
    // set query param: categories
    // ==================================================

    url += buildListFilter(ddCategories, chkAllCategories);
     
    // ==================================================

    fetch(url)
        .then((res) => res.json())
        .then((data) => {
            loadHolidays(data);
            document.body.removeAttribute("data-loading");
        })
        .catch((err) => {
            document.body.removeAttribute("data-loading");
            console.error(err);
        });
}

function loadHolidays(holidayItems) {
    const template = document.querySelector("template#holiday-item");
    const parent = document.querySelector("#holiday-container");

    parent.innerHTML = "";

    if (!holidayItems || holidayItems.length == 0) {
        return (parent.innerHTML = `
            <div class="container alert-no-result alert alert-secondary" role="alert"> 
                    <h1>Oops!</h1>
                    <h2>No packages found.</h2>
                    <p>
                        Sorry! None of our travel deals was found with the choosen filters.

                    </p>

                    <p>
                        <strong>
                             Please, try using different combination of filters.
                        </strong>
                    </p>
            </div>
            `);
    }

    holidayItems.forEach(({ travelPackage, totalInCart }, index) => {
        const totalItems = totalInCart > 0 ? ` (${totalInCart})` : "";

        var clone = template.content.cloneNode(true);
        clone.firstElementChild.setAttribute("data-id", travelPackage.id);

        clone.querySelector(".item-image").src = travelPackage.image;
        clone.querySelector(".item-title").textContent =
            travelPackage.name + totalItems;
        clone.querySelector(".item-content").textContent =
            travelPackage.description;

        // display Travel Package Link
        const detailLinks = clone.querySelectorAll(".detail-link");
        detailLinks.forEach((link) =>
            link.setAttribute("href", "/holiday/detail?id=" + travelPackage.id)
        );

        // add cart item link
        const addTravelLink = clone.querySelector(".add-travel-link");
        setCartActionLink(addTravelLink, travelPackage.id, true);

        // remove-cart item link
        const removeTravelLink = clone.querySelector(".remove-travel-link");
        setCartActionLink(removeTravelLink, travelPackage.id, false);
        if (removeTravelLink)
            removeTravelLink.style.display = !totalItems ? "none" : "";

        parent.appendChild(clone);
    });
}

function setCartActionLink(updateTravelLink, travelPackageId, isAdd) {
    if (updateTravelLink) {
        updateTravelLink.setAttribute("href", "");
        updateTravelLink.setAttribute("data-id", travelPackageId);
        updateTravelLink.addEventListener("click", (e) =>
            updateTravelCart(e, isAdd)
        );
    }
}

function updateTravelCart(e, isAdd = true) {
    e.preventDefault();

    document.body.setAttribute("data-loading", "true");

    const errorMessage = (err) => {
        alert("The service is not available right now.\nPlease try later.");
        console.error("Error: " + err);
    };

    // url Params
    let ACTION = isAdd ? "addtocart" : "remofromcart";

    const { id } = e.target.dataset;

    fetch(`/api/${ACTION}?tpId=${id}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    })
        .then((res) => {
            if (res.status != 200) {
                return errorMessage("status code: " + status);
            }
            return res.json();
        })
        .then((res) => {
            const { cart } = res;
            const { travelPackage } = cart;

            const card = document.querySelector(`[data-id="${travelPackage.id}"]`);

            // update title with quantity
            const blockTitle = card.querySelector(`.item-title`);
            blockTitle.innerText =
                travelPackage.name + (cart.qty == 0 ? "" : ` (${cart.qty})`);

            // display remove link if quantity > 0;
            const removeTravelLink = card.querySelector(`.remove-travel-link`);
            removeTravelLink.style.display = cart.qty == 0 ? "none" : "";
        })
        .then(() => reloadCart())
        .catch((err) => {
            errorMessage(err);
        })
        .finally(() => {
            document.body.removeAttribute("data-loading");
        });
}

function reloadCart() {
    fetch("/api/getcartsummary")
        .then((res) => {
            return res.text();
        })
        .then((html) => {
            const cartBlock = document.querySelector("#cart");
            cartBlock.innerHTML = html;
        });
}
