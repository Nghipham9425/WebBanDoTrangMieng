// Modal Fix Script
console.log("Modal fix script loaded!");

// Global functions
window.showModal = function () {
  console.log("showModal called");
  $("#authModal").addClass("show");
  $("body").addClass("modal-open");
};

window.closeModal = function () {
  console.log("closeModal called");
  $("#authModal").removeClass("show");
  $("body").removeClass("modal-open");
};

$(document).ready(function () {
  console.log("Modal fix ready!");

  // Tab switching
  $(".auth-tab").on("click", function () {
    console.log("Tab clicked:", $(this).data("target"));
    const target = $(this).data("target");

    // Update active tab
    $(".auth-tab").removeClass("active");
    $(this).addClass("active");

    // Update active form
    $(".auth-form").removeClass("active");
    $("#" + target).addClass("active");
  });

  // Close modal button
  $(".auth-close").on("click", function () {
    console.log("Close button clicked");
    closeModal();
  });

  // Close when clicking outside modal
  $("#authModal").on("click", function (e) {
    if (e.target === this) {
      console.log("Clicked outside modal");
      closeModal();
    }
  });

  // Escape key to close
  $(document).on("keydown", function (e) {
    if (e.key === "Escape" && $("#authModal").hasClass("show")) {
      console.log("Escape key pressed");
      closeModal();
    }
  });
});
