document.addEventListener("DOMContentLoaded", () => {
  var url = new URL(window.location.href);
  var id = url.searchParams.get("id");

  loadHoliday(id);
});

function loadHoliday(id) {
    const holiday = holidayItems.find(item=> item.id == id);
    const itemImage = document.querySelector(".item-image");
    itemImage.setAttribute("src", holiday.imageUrl);
}
