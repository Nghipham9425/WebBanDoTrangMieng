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

/* ==========================================
   AUTH MODAL - BOOTSTRAP MODAL INTEGRATION
   ========================================== */

$(document).ready(function () {
  "use strict";

  // ==========================================
  // BOOTSTRAP MODAL INTEGRATION
  // ==========================================

  // Show modal when user icon is clicked (for non-logged in users)
  $(".user-toggle").click(function (e) {
    e.preventDefault();
    $("#authModal").modal("show");
  });

  // Bootstrap tabs work automatically, no need for manual initialization

  // ==========================================
  // FORM HANDLING
  // ==========================================

  // Login form submission
  $("#loginTab form").on("submit", function (e) {
    e.preventDefault();

    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const email = form.find('input[type="email"]').val();
    const password = form.find('input[type="password"]').val();

    // Basic validation
    if (!email || !password) {
      showAlert("Vui lòng điền đầy đủ thông tin!", "danger");
      return;
    }

    // Show loading state
    showLoadingState(submitBtn, true);

    // Simulate AJAX request (replace with actual login logic)
    setTimeout(function () {
      // Hide loading state
      showLoadingState(submitBtn, false);

      // Example success
      showAlert("Đăng nhập thành công!", "success");

      // Close modal after 1.5 seconds
      setTimeout(function () {
        const authModal = bootstrap.Modal.getInstance(
          document.getElementById("authModal")
        );
        authModal.hide();

        // Reload page or redirect
        // window.location.reload();
      }, 1500);
    }, 2000);
  });

  // Register form submission
  $("#registerTab form").on("submit", function (e) {
    e.preventDefault();

    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const fullName = form.find('input[type="text"]').val();
    const email = form.find('input[type="email"]').val();
    const password = form.find('input[type="password"]').eq(0).val();
    const confirmPassword = form.find('input[type="password"]').eq(1).val();

    // Basic validation
    if (!fullName || !email || !password || !confirmPassword) {
      showAlert("Vui lòng điền đầy đủ thông tin!", "danger");
      return;
    }

    if (password !== confirmPassword) {
      showAlert("Mật khẩu xác nhận không khớp!", "danger");
      return;
    }

    if (password.length < 6) {
      showAlert("Mật khẩu phải có ít nhất 6 ký tự!", "danger");
      return;
    }

    // Show loading state
    showLoadingState(submitBtn, true);

    // Simulate AJAX request (replace with actual register logic)
    setTimeout(function () {
      // Hide loading state
      showLoadingState(submitBtn, false);

      // Example success
      showAlert("Đăng ký thành công! Vui lòng đăng nhập.", "success");

      // Switch to login tab after 2 seconds
      setTimeout(function () {
        const loginTab = new bootstrap.Tab(
          document.querySelector('[data-bs-target="#loginTab"]')
        );
        loginTab.show();

        // Clear register form
        form[0].reset();

        // Remove any existing alerts
        $(".alert").remove();
      }, 2000);
    }, 2000);
  });

  // ==========================================
  // PASSWORD VISIBILITY TOGGLE
  // ==========================================

  // Add password toggle buttons dynamically
  $('input[type="password"]').each(function () {
    const input = $(this);
    const container = input.parent();

    if (!container.find(".password-toggle").length) {
      container.addClass("position-relative");
      container.append(`
                <button type="button" class="btn btn-sm position-absolute top-50 end-0 translate-middle-y me-2 password-toggle" style="background: none; border: none; z-index: 5;">
                    <i class="fas fa-eye text-muted"></i>
                </button>
            `);
    }
  });

  // Password toggle functionality
  $(document).on("click", ".password-toggle", function () {
    const button = $(this);
    const input = button.siblings('input[type="password"], input[type="text"]');
    const icon = button.find("i");

    if (input.attr("type") === "password") {
      input.attr("type", "text");
      icon.removeClass("fa-eye").addClass("fa-eye-slash");
    } else {
      input.attr("type", "password");
      icon.removeClass("fa-eye-slash").addClass("fa-eye");
    }
  });

  // ==========================================
  // UTILITY FUNCTIONS
  // ==========================================

  function showAlert(message, type) {
    // Remove existing alerts
    $(".alert").remove();

    const alertHtml = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                <i class="fas fa-${
                  type === "success" ? "check-circle" : "exclamation-triangle"
                } me-2"></i>
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;

    // Add alert to active tab
    $(".tab-pane.active").prepend(alertHtml);

    // Auto-dismiss after 5 seconds
    setTimeout(function () {
      $(".alert").fadeOut();
    }, 5000);
  }

  function showLoadingState(button, isLoading) {
    if (isLoading) {
      button.addClass("loading");
      button.prop("disabled", true);
      button.find("i").hide();
      button.append(`
                <span class="spinner-border spinner-border-sm me-2" role="status">
                    <span class="visually-hidden">Loading...</span>
                </span>
            `);
    } else {
      button.removeClass("loading");
      button.prop("disabled", false);
      button.find("i").show();
      button.find(".spinner-border").remove();
    }
  }

  // ==========================================
  // FORM ENHANCEMENTS
  // ==========================================

  // Real-time validation
  $('input[type="email"]').on("blur", function () {
    const email = $(this).val();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (email && !emailRegex.test(email)) {
      $(this).addClass("is-invalid");
    } else {
      $(this).removeClass("is-invalid");
    }
  });

  // Clear validation states when typing
  $("input").on("input", function () {
    $(this).removeClass("is-invalid is-valid");
  });

  // Reset form when modal is hidden
  $("#authModal").on("hidden.bs.modal", function () {
    // Clear all forms
    $(this).find("form")[0].reset();

    // Remove validation states
    $(this).find("input").removeClass("is-invalid is-valid");

    // Remove alerts
    $(this).find(".alert").remove();

    // Reset to login tab
    const loginTab = new bootstrap.Tab(
      document.querySelector('[data-bs-target="#loginTab"]')
    );
    loginTab.show();

    // Reset password visibility
    $(this)
      .find('input[type="text"]')
      .each(function () {
        if ($(this).siblings(".password-toggle").length) {
          $(this).attr("type", "password");
          $(this)
            .siblings(".password-toggle")
            .find("i")
            .removeClass("fa-eye-slash")
            .addClass("fa-eye");
        }
      });
  });

  // ==========================================
  // SMOOTH ANIMATIONS
  // ==========================================

  // Add smooth transitions to tab switching
  $('[data-bs-toggle="tab"]').on("shown.bs.tab", function (e) {
    $(e.target.getAttribute("data-bs-target")).addClass("fade-in-up");

    setTimeout(function () {
      $(".tab-pane").removeClass("fade-in-up");
    }, 600);
  });

  // Modal show animation
  $("#authModal").on("show.bs.modal", function () {
    $(this).find(".modal-dialog").addClass("bounce-in");

    setTimeout(function () {
      $(".modal-dialog").removeClass("bounce-in");
    }, 800);
  });

  // ==========================================
  // ACCESSIBILITY IMPROVEMENTS
  // ==========================================

  // Focus management
  $("#authModal").on("shown.bs.modal", function () {
    $(this).find(".tab-pane.active input:first").focus();
  });

  // Keyboard navigation
  $("#authModal").on("keydown", function (e) {
    if (e.key === "Tab") {
      // Custom tab handling if needed
    }
  });

  console.log("✅ Auth Modal initialized with Bootstrap integration");
});

/* ==========================================
   GLOBAL FUNCTIONS FOR EXTERNAL USE
   ========================================== */

// Function to show auth modal programmatically
function showAuthModal(tab = "login") {
  const authModal = new bootstrap.Modal(document.getElementById("authModal"));
  authModal.show();

  // Switch to specified tab
  if (tab === "register") {
    setTimeout(function () {
      const registerTab = new bootstrap.Tab(
        document.querySelector('[data-bs-target="#registerTab"]')
      );
      registerTab.show();
    }, 100);
  }
}

// Function to handle successful login (to be called from server response)
function handleLoginSuccess(userData) {
  const authModal = bootstrap.Modal.getInstance(
    document.getElementById("authModal")
  );
  if (authModal) {
    authModal.hide();
  }

  // Update UI with user data
  updateUserUI(userData);

  // Show success message
  showToast("Đăng nhập thành công!", "success");
}

// Function to update user UI after login
function updateUserUI(userData) {
  // Update navigation
  $(".user-toggle").replaceWith(`
        <a class="nav-link dropdown-toggle text-success" href="#" role="button" 
           data-bs-toggle="dropdown" aria-expanded="false">
            <i class="fas fa-user"></i>
            <span class="d-none d-md-inline ms-1">${userData.name}</span>
        </a>
    `);

  // Add user dropdown menu if not exists
  // This would be handled by the server-side template in a real application
}

// Function to show toast notifications
function showToast(message, type = "info") {
  // Create toast if it doesn't exist
  if (!$("#toast-container").length) {
    $("body").append(`
            <div id="toast-container" class="toast-container position-fixed top-0 end-0 p-3" style="z-index: 9999;"></div>
        `);
  }

  const toastHtml = `
        <div class="toast align-items-center text-white bg-${
          type === "success" ? "success" : type === "error" ? "danger" : "info"
        } border-0" role="alert">
            <div class="d-flex">
                <div class="toast-body">
                    <i class="fas fa-${
                      type === "success"
                        ? "check-circle"
                        : type === "error"
                        ? "times-circle"
                        : "info-circle"
                    } me-2"></i>
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;

  const toastElement = $(toastHtml);
  $("#toast-container").append(toastElement);

  const toast = new bootstrap.Toast(toastElement[0]);
  toast.show();

  // Remove from DOM after hidden
  toastElement.on("hidden.bs.toast", function () {
    $(this).remove();
  });
}
