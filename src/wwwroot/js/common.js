function setActivePageLink(page) {
    const parseLink = (url) => {
        let index = url.lastIndexOf("/");
        let page = url.substr(index + 1);
        return page;
    };

    let activePage = parseLink(window.location.pathname);

    const links = document.querySelectorAll("nav li a");
    links.forEach((li) => {
        const anchor = li;
        const linkPage = parseLink(anchor.href);
        if (linkPage === activePage) {
            li.parentNode.classList.add("active");
        }
    });
}

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
    const pageLoader = `<div class="loader-container">
  <div class="loader"></div>
</div>`;

    document.body.insertAdjacentHTML("afterbegin", pageLoader);
}

function setLinkListener() {
    const links = document.querySelectorAll(
        "nav a:not(.navbar-brand):not([role=button])"
    );
    links.forEach((a) =>
        a.addEventListener("click", (e) => {
           if(!e.metaKey)
                document.body.setAttribute("data-loading", "true");
        })
    );
}

