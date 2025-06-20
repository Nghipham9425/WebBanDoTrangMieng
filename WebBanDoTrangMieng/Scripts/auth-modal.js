/*
 * ==========================================
 * AUTH-MODAL.JS - Authentication Modal JavaScript
 * Website: Đồ Tráng Miệng
 * ==========================================
 */

/*
 * OPTIMIZED AUTH-MODAL.JS
 */

$(document).ready(function () {
  setTimeout(setupModalEvents, 100);
});

function setupModalEvents() {
  // MODAL CONTROLS
  $(".user-toggle")
    .off("click.authmodal")
    .on("click.authmodal", function (e) {
      e.preventDefault();
      if ($(this).closest(".user-dropdown").length === 0) {
        openModal();
      }
      return false;
    });

  function openModal() {
    $("#authModal").css("display", "flex").addClass("show");
    $("body").addClass("modal-open");
  }

  function closeModal() {
    $("#authModal").removeClass("show");
    $("body").removeClass("modal-open");
    setTimeout(() => $("#authModal").hide(), 300);
  }

  // CLOSE HANDLERS
  $(".auth-close").on("click", function (e) {
    e.preventDefault();
    closeModal();
  });

  $("#authModal").on("click", function (e) {
    if (e.target === this) closeModal();
  });

  $(document).on("keydown", function (e) {
    if (e.key === "Escape" && $("#authModal").hasClass("show")) {
      closeModal();
    }
  });

  // TAB SWITCHING
  $(".auth-tab").on("click", function () {
    const target = $(this).data("target");
    $(".auth-tab").removeClass("active");
    $(this).addClass("active");
    $(".auth-form").removeClass("active");
    $("#" + target).addClass("active");
  });

  // FORGOT PASSWORD
  $("#forgotPassword").on("click", function (e) {
    e.preventDefault();
    $(".auth-tabs").hide();
    $(".auth-form").removeClass("active");
    $("#resetPasswordForm").addClass("active");
  });

  $("#backToLogin").on("click", function (e) {
    e.preventDefault();
    $(".auth-tabs").show();
    $(".auth-form").removeClass("active");
    $("#loginForm").addClass("active");
    $(".auth-tab").removeClass("active");
    $('.auth-tab[data-target="loginForm"]').addClass("active");
  });

  // PASSWORD TOGGLE
  $(".toggle-password").on("click", function () {
    const targetId = $(this).data("target");
    const passwordField = $("#" + targetId);
    const type =
      passwordField.attr("type") === "password" ? "text" : "password";
    passwordField.attr("type", type);
    $(this).toggleClass("fa-eye fa-eye-slash");
  });

  // FORM SUBMISSIONS
  $("#loginForm form").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const email = $("#loginEmail").val().trim();
    const password = $("#loginPassword").val().trim();
    const rememberMe = $("#rememberMe").is(":checked");

    if (!email || !password) {
      showMessage("Vui lòng nhập đầy đủ thông tin!", "error");
      return;
    }

    submitBtn.prop("disabled", true).text("Đang đăng nhập...");

    $.ajax({
      url: "/User/LoginAjax",
      type: "POST",
      data: { Email: email, Password: password, RememberMe: rememberMe },
      success: function (response) {
        if (response.success) {
          showMessage(response.message, "success");
          setTimeout(() => {
            closeModal();
            location.reload();
          }, 1000);
        } else {
          showMessage(response.message, "error");
        }
      },
      error: function () {
        showMessage("Có lỗi xảy ra khi đăng nhập!", "error");
      },
      complete: function () {
        submitBtn.prop("disabled", false).text("ĐĂNG NHẬP");
      },
    });
  });

  $("#registerForm form").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const fullName = $("#fullName").val().trim();
    const email = $("#registerEmail").val().trim();
    const password = $("#registerPassword").val().trim();
    const confirmPassword = $("#confirmPassword").val().trim();

    // Validation
    if (!fullName || !email || !password || !confirmPassword) {
      showMessage("Vui lòng nhập đầy đủ thông tin!", "error");
      return;
    }

    if (password !== confirmPassword) {
      showMessage("Mật khẩu xác nhận không khớp!", "error");
      return;
    }

    if (password.length < 6) {
      showMessage("Mật khẩu phải có ít nhất 6 ký tự!", "error");
      return;
    }

    submitBtn.prop("disabled", true).text("Đang đăng ký...");

    $.ajax({
      url: "/User/RegisterAjax",
      type: "POST",
      data: {
        FullName: fullName,
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword,
      },
      success: function (response) {
        if (response.success) {
          showMessage(response.message, "success");
          setTimeout(() => {
            $(".auth-tab").removeClass("active");
            $('.auth-tab[data-target="loginForm"]').addClass("active");
            $(".auth-form").removeClass("active");
            $("#loginForm").addClass("active");
          }, 1500);
        } else {
          showMessage(response.message, "error");
        }
      },
      error: function () {
        showMessage("Có lỗi xảy ra khi đăng ký!", "error");
      },
      complete: function () {
        submitBtn.prop("disabled", false).text("ĐĂNG KÝ");
      },
    });
  });

  $("#resetPasswordForm form").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const email = $("#resetEmail").val().trim();

    if (!email) {
      showMessage("Vui lòng nhập email!", "error");
      return;
    }

    submitBtn.prop("disabled", true).text("Đang gửi...");

    $.ajax({
      url: "/User/ForgotPassword",
      type: "POST",
      data: { Email: email },
      success: function (response) {
        if (response.success) {
          showMessage(
            "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn!",
            "success"
          );
        } else {
          showMessage(
            response.message || "Email không tồn tại trong hệ thống!",
            "error"
          );
        }
      },
      error: function () {
        showMessage("Có lỗi xảy ra khi gửi email!", "error");
      },
      complete: function () {
        submitBtn.prop("disabled", false).text("GỬI LIÊN KẾT");
      },
    });
  });

  // UTILITY FUNCTIONS
  function showMessage(message, type) {
    const alert = $(`
      <div class="auth-alert ${
        type === "success" ? "alert-success" : "alert-danger"
      }">
        <i class="fas ${
          type === "success" ? "fa-check-circle" : "fa-exclamation-circle"
        }"></i>
        ${message}
      </div>
    `);

    $(".auth-content .auth-alert").remove();
    $(".auth-content").prepend(alert);

    setTimeout(() => alert.fadeOut(() => alert.remove()), 5000);
  }

  function positionUserDropdown() {
    const dropdown = $(".user-dropdown-menu");
    if (dropdown.length && dropdown.is(":visible")) {
      const windowWidth = $(window).width();
      const dropdownWidth = dropdown.outerWidth();

      if (windowWidth <= 768) {
        dropdown.css({
          position: "fixed",
          top: "70px",
          right: "10px",
          left: "auto",
          width: "250px",
        });
      } else {
        dropdown.css({
          position: "fixed",
          top: "70px",
          right: "20px",
          left: "auto",
          width: "280px",
        });
      }
    }
  }

  $(window).on("resize scroll", positionUserDropdown);
  positionUserDropdown();
}
