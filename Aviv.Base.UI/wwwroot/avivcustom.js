

/* full screen */
var elem = document.documentElement;
function openFullscreen() {
  let open = document.querySelector(".full-screen-open");
  let close = document.querySelector(".full-screen-close");

  if (
    !document.fullscreenElement &&
    !document.webkitFullscreenElement &&
    !document.msFullscreenElement
  ) {
    if (elem.requestFullscreen) {
      elem.requestFullscreen();
    } else if (elem.webkitRequestFullscreen) {
      /* Safari */
      elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) {
      /* IE11 */
      elem.msRequestFullscreen();
    }
    close.classList.add("d-block");
    close.classList.remove("d-none");
    open.classList.add("d-none");
  } else {
    if (document.exitFullscreen) {
      document.exitFullscreen();
    } else if (document.webkitExitFullscreen) {
      /* Safari */
      document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) {
      /* IE11 */
      document.msExitFullscreen();
    }
    close.classList.remove("d-block");
    open.classList.remove("d-none");
    close.classList.add("d-none");
    open.classList.add("d-block");
  }
}
/* full screen */

/* Toastr */
window.showToastr = function (type, message) {
    if (type == "success") {
        toastr.success(message);
    }
    if (type == "error") {
        toastr.error(message);
    }
}  
/* Toastr */



/* SweetAlert */

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
    },
    buttonsStyling: false
});

window.ShowSwal = function (type, message) {
    if (type == "success") {
        Swal.fire({
            title: "Good job!",
            text: message,
            icon: "success"
        });
    }
    if (type == "error") {
        Swal.fire({
            title: "Task Failed!",
            text: message,
            icon: "error"
        });
    }

}

    window.showConfirm = function (message) {
    return new Promise(function (resolve) {
        Swal.fire({
            title: 'Confirm',
            text: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true,
            allowOutsideClick: false, // Block outside clicks
            //grow: 'fullscreen', // Use the grow animation
            customClass: {
                confirmButton: 'btn btn-sm btn-primary',
                cancelButton: 'btn btn-sm btn-light',
                actions: 'custom-actions-class'
            }
        }).then((result) => {
            resolve(result.isConfirmed);
        });
    });
}

window.ShowDeleteConfirmation = function () {
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your imaginary file is safe :)",
                icon: "error"
            });
        }
    });
}


/* SweetAlert */

window.ShowMixinSimple = function (icon, title) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,

        //customClass: {
        //    container: 'small-toast'
        //},
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: 'success',
        title: 'Signed in successfully'
    })

}

window.showCustomAlert = function () {
    const alertHtml = `
    <div id="customAlert" class="alert alert-primary alert-dismissible fade show custom-alert-icon shadow-sm position-fixed top-0 end-0 m-3" role="alert">
        <svg class="svg-primary" xmlns="http://www.w3.org/2000/svg" height="1.5rem" viewBox="0 0 24 24" width="1.5rem" fill="#000000">
            <path d="M0 0h24v24H0z" fill="none"></path>
            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z"></path>
        </svg> A customized primary alert with an icon
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
            <i class="bi bi-x"></i>
        </button>
    </div>`;
    document.body.insertAdjacentHTML('beforeend', alertHtml);

    setTimeout(function () {
        const alertElement = document.getElementById("customAlert");
        if (alertElement) {
            alertElement.classList.remove("show");
            alertElement.classList.add("hide");
            setTimeout(function () {
                alertElement.remove();
            }, 150); // Allow time for fade out
        }
    }, 3000); // 3000 milliseconds = 3 seconds
}


window.showCustomAlertSVG = function (alertType, alertMessage) {

const alertHtml = `
<div id="customAlert" class="alert alert-${alertType} alert-dismissible fade show custom-alert-icon shadow-sm position-fixed top-0 end-0 m-3" role="alert">
    <svg class="svg-${alertType}" xmlns="http://www.w3.org/2000/svg" height="1.5rem" viewBox="0 0 24 24" width="1.5rem" fill="#000000">
        <path d="M0 0h24v24H0z" fill="none"></path>
        <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z"></path>
    </svg> ${alertMessage}
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
        <i class="bi bi-x"></i>
    </button>
</div>`;
    document.body.insertAdjacentHTML('beforeend', alertHtml);

    setTimeout(function () {
        const alertElement = document.getElementById("customAlert");
        if (alertElement) {
            alertElement.classList.remove("show");
            alertElement.classList.add("hide");
            setTimeout(function () {
                alertElement.remove();
            }, 150); // Allow time for fade out
        }
    }, 1000); // 3000 milliseconds = 3 seconds
}

// Modal & Offcanvas show

// Bootstrap Modal Functions
window.showModal = function (modalId) {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
        var modal = new bootstrap.Modal(modalElement);
        modal.show();
    }
};

window.hideModal = function (modalId) {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
        var modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();
        }
    }
};

// Bootstrap Offcanvas Functions
window.showOffcanvas = function (offcanvasId) {
    var offcanvasElement = document.getElementById(offcanvasId);
    if (offcanvasElement) {
        var offcanvas = new bootstrap.Offcanvas(offcanvasElement);
        offcanvas.show();
    }
};

window.hideOffcanvas = function (offcanvasId) {
    var offcanvasElement = document.getElementById(offcanvasId);
    if (offcanvasElement) {
        var offcanvas = bootstrap.Offcanvas.getInstance(offcanvasElement);
        if (offcanvas) {
            offcanvas.hide();
        }
    }
};

// Function to handle image preview before upload
window.previewImage = function (inputId, imgId) {
    var input = document.getElementById(inputId);
    var img = document.getElementById(imgId);

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            img.src = e.target.result;
        };

        reader.readAsDataURL(input.files[0]);
    }
};


//

// SweetAlert2 Toast function
window.showToast = function (message, type = 'success', duration = 3000) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        content: 'swalmixin-content-class',
        timer: duration,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    Toast.fire({
        icon: type,
        title: message
    });
}

// Delete confirmation helper
window.confirmDelete1 = async function () {
    const result = await Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    });

    return result.isConfirmed;
}

// Custom validation for date inputs
function validateDateInput(input) {
    const value = input.value;
    const isValid = /^\d{4}$/.test(value);

    if (value && !isValid) {
        input.setCustomValidity('Please enter a valid 4-digit year');
    } else {
        input.setCustomValidity('');
    }
}

// Create a namespace for the SweetAlert functionality
window.sweetAlert = {
    confirmDelete: function (title, text, confirmButtonText) {
        // Return a promise that resolves with the user's choice
        return new Promise((resolve, reject) => {
            Swal.fire({
                title: title,
                text: text,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: confirmButtonText,
                cancelButtonText: 'Cancel'
            }).then((result) => {
                resolve(result.isConfirmed);
            });
        });
    }
};


// Function to show confirmation dialog
function swalConfirm(title, text, icon) {
    return new Promise((resolve) => {
        Swal.fire({
            title: title,
            text: text,
            icon: icon,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            resolve(result.isConfirmed);
        });
    });
}

// Function to show success message
function swalSuccess(title, text) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'success',
        timer: 2000,
        showConfirmButton: false
    });
}

// Function to show error message
function swalError(title, text) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'error',
        confirmButtonColor: '#3085d6'
    });
}


// Initialize SweetAlert functions for Blazor interop
window.sweetAlert = {
    confirmDelete2: function (title, text, confirmButtonText) {
        return Swal.fire({
            title: title,
            text: text,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: confirmButtonText,
            cancelButtonText: 'Cancel'
        }).then((result) => {
            return result.isConfirmed;
        });
    },

    toast: function (message, icon) {
        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer);
                toast.addEventListener('mouseleave', Swal.resumeTimer);
            }
        });

        Toast.fire({
            icon: icon,
            title: message
        });
    }
};

// File download function

// Document JavaScript Interop functions
// Document JavaScript Interop functions
// Trigger click on an element
window.clickElement = function (element) {
    if (element) {
        element.click();
    }
};

// SweetAlert2 confirmation for document deletion
window.showDeleteConfirmationDoc = function () {
    return new Promise((resolve) => {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            resolve(result.isConfirmed);
        });
    });
};

// Show loading spinner
window.showLoading = function () {
    Swal.fire({
        title: 'Processing...',
        html: 'Please wait',
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });
};

// Hide loading spinner
window.hideLoading = function () {
    Swal.close();
};


// Toast notification helper
window.showToastrTkt = function (message, type) {
    // Check if Toastr is available
    if (typeof toastr !== 'undefined') {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: 3000
        };

        switch (type) {
            case 'success':
                toastr.success(message);
                break;
            case 'error':
                toastr.error(message);
                break;
            case 'warning':
                toastr.warning(message);
                break;
            case 'info':
                toastr.info(message);
                break;
            default:
                toastr.info(message);
        }
    } else {
        // Fallback alert if Toastr is not available
        alert(message);
    }
};

// Function to check if SweetAlert2 is loaded and load it if not
window.ensureSweetAlert = function () {
    return new Promise((resolve, reject) => {
        if (typeof Swal !== 'undefined') {
            resolve();
            return;
        }

        // If SweetAlert2 is not loaded, load it
        const script = document.createElement('script');
        script.src = 'https://cdn.jsdelivr.net/npm/sweetalert2@11';
        script.onload = () => resolve();
        script.onerror = (error) => reject(new Error(`Failed to load SweetAlert2: ${error}`));
        document.head.appendChild(script);
    });
};

// Example of how to show a confirmation dialog
window.showConfirmation = async function (title, text, icon) {
    await window.ensureSweetAlert();

    return await Swal.fire({
        title: title || 'Are you sure?',
        text: text || "You won't be able to revert this!",
        icon: icon || 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, proceed!'
    });
};

window.showToastPrd = function (message, type) {
    Swal.fire({
        toast: true,
        position: 'top-end',
        icon: type,
        title: message,
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
};

