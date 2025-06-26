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
  // MODAL CONTROLS - Mở modal khi click vào icon user
  $(".user-toggle")
    .off("click.authmodal")
    .on("click.authmodal", function (e) {
      e.preventDefault();
      if ($(this).closest(".dropdown").length === 0) {
        openModal();
      }
      return false;
    });

  function openModal() {
    $("#authModal").modal("show");
  }

  function closeModal() {
    $("#authModal").modal("hide");
    clearForms();
  }

  function clearForms() {
    $("#loginFormSubmit")[0].reset();
    $("#registerFormSubmit")[0].reset();
    $("#resetPasswordFormSubmit")[0].reset();
    hideAlert();
  }

  // TAB SWITCHING
  $(".auth-tab").on("click", function (e) {
    e.preventDefault();
    const target = $(this).data("target");
    $(".auth-tab").removeClass("active");
    $(this).addClass("active");
    $(".auth-form").removeClass("show active");
    $("#" + target).addClass("show active");
    hideAlert();
  });

  // FORGOT PASSWORD
  $("#forgotPassword").on("click", function (e) {
    e.preventDefault();
    $(".auth-tabs").hide();
    $(".auth-form").removeClass("show active");
    $("#resetPasswordForm").addClass("show active").show();
    hideAlert();
  });

  $("#backToLogin").on("click", function (e) {
    e.preventDefault();
    $(".auth-tabs").show();
    $(".auth-form").removeClass("show active");
    $("#loginForm").addClass("show active");
    $(".auth-tab").removeClass("active");
    $('.auth-tab[data-target="loginForm"]').addClass("active");
    hideAlert();
  });

  // FORM SUBMISSIONS
  $("#loginFormSubmit").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const email = $("#loginEmail").val().trim();
    const password = $("#loginPassword").val().trim();
    const rememberMe = $("#rememberMe").is(":checked");

    if (!email || !password) {
      showAlert("Vui lòng nhập đầy đủ thông tin!", "danger");
      return;
    }

    // Disable button and show loading
    const originalText = submitBtn.html();
    submitBtn
      .prop("disabled", true)
      .html('<i class="fas fa-spinner fa-spin me-2"></i>Đang đăng nhập...');

    // Get antiforgery token
    const token = $(
      "#loginFormSubmit input[name='__RequestVerificationToken']"
    ).val();

    $.ajax({
      url: "/User/LoginAjax",
      type: "POST",
      data: {
        Email: email,
        Password: password,
        RememberMe: rememberMe,
        __RequestVerificationToken: token,
      },
      success: function (response) {
        if (response.success) {
          showAlert(response.message, "success");
          setTimeout(() => {
            closeModal();
            location.reload(); // Reload để cập nhật UI
          }, 1000);
        } else {
          showAlert(response.message, "danger");
        }
      },
      error: function (xhr, status, error) {
        console.error("Login error:", xhr.responseText);
        showAlert("Có lỗi xảy ra khi đăng nhập!", "danger");
      },
      complete: function () {
        submitBtn.prop("disabled", false).html(originalText);
      },
    });
  });

  $("#registerFormSubmit").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const userName = $("#fullName").val().trim();
    const email = $("#registerEmail").val().trim();
    const phone = $("#phone").val().trim();
    const password = $("#registerPassword").val().trim();
    const confirmPassword = $("#confirmPassword").val().trim();

    // Validation
    if (!userName || !email || !password || !confirmPassword) {
      showAlert("Vui lòng nhập đầy đủ thông tin bắt buộc!", "danger");
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

    // Email validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      showAlert("Email không hợp lệ!", "danger");
      return;
    }

    // Disable button and show loading
    const originalText = submitBtn.html();
    submitBtn
      .prop("disabled", true)
      .html('<i class="fas fa-spinner fa-spin me-2"></i>Đang đăng ký...');

    // Get antiforgery token
    const regToken = $(
      "#registerFormSubmit input[name='__RequestVerificationToken']"
    ).val();

    $.ajax({
      url: "/User/RegisterAjax",
      type: "POST",
      data: {
        UserName: userName,
        Email: email,
        Phone: phone,
        Password: password,
        ConfirmPassword: confirmPassword,
        __RequestVerificationToken: regToken,
      },
      success: function (response) {
        if (response.success) {
          showAlert(response.message, "success");
          setTimeout(() => {
            closeModal();
            location.reload(); // Reload để cập nhật UI
          }, 1000);
        } else {
          showAlert(response.message, "danger");
        }
      },
      error: function (xhr, status, error) {
        console.error("Register error:", xhr.responseText);
        showAlert("Có lỗi xảy ra khi đăng ký!", "danger");
      },
      complete: function () {
        submitBtn.prop("disabled", false).html(originalText);
      },
    });
  });

  $("#resetPasswordFormSubmit").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const submitBtn = form.find('button[type="submit"]');
    const email = $("#resetEmail").val().trim();

    if (!email) {
      showAlert("Vui lòng nhập email!", "danger");
      return;
    }

    // Email validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      showAlert("Email không hợp lệ!", "danger");
      return;
    }

    // Disable button and show loading
    const originalText = submitBtn.html();
    submitBtn
      .prop("disabled", true)
      .html('<i class="fas fa-spinner fa-spin me-2"></i>Đang gửi...');

    // Simulate API call (replace with actual implementation)
    setTimeout(() => {
      showAlert(
        "Link reset mật khẩu đã được gửi đến email của bạn!",
        "success"
      );
      submitBtn.prop("disabled", false).html(originalText);
    }, 2000);
  });

  // ALERT FUNCTIONS
  function showAlert(message, type) {
    const alertDiv = $("#authAlerts");
    const alertElement = alertDiv.find(".alert");

    // Remove existing classes and add new type
    alertElement
      .removeClass("alert-success alert-danger alert-warning alert-info")
      .addClass("alert-" + type);

    $("#authMessage").text(message);
    alertDiv.show();

    // Auto hide after 5 seconds for success messages
    if (type === "success") {
      setTimeout(() => {
        hideAlert();
      }, 5000);
    }
  }

  function hideAlert() {
    $("#authAlerts").hide();
  }

  // Close alert when clicking X
  $(document).on("click", "#authAlerts .btn-close", function () {
    hideAlert();
  });

  // Clear forms when modal is hidden
  $("#authModal").on("hidden.bs.modal", function () {
    clearForms();
  });
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
  $("#authModal").modal("show");
  if (tab === "register") {
    $(".auth-tab").removeClass("active");
    $('.auth-tab[data-target="registerForm"]').addClass("active");
    $(".auth-form").removeClass("show active");
    $("#registerForm").addClass("show active");
  }
}

// Function to handle successful login (to be called from server response)
function handleLoginSuccess(userData) {
  // Update UI after successful login
  location.reload();
}

// Function to show toast notifications
function showToast(message, type = "info") {
  // Bootstrap toast implementation
  const toastHtml = `
        <div class="toast align-items-center text-white bg-${
          type === "error" ? "danger" : type
        } border-0" role="alert">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;

  // Create toast container if not exists
  if (!$("#toastContainer").length) {
    $("body").append(
      '<div id="toastContainer" class="toast-container position-fixed top-0 end-0 p-3"></div>'
    );
  }

  const $toast = $(toastHtml);
  $("#toastContainer").append($toast);

  const toast = new bootstrap.Toast($toast[0]);
  toast.show();

  // Remove toast element after it's hidden
  $toast.on("hidden.bs.toast", function () {
    $(this).remove();
  });
}
