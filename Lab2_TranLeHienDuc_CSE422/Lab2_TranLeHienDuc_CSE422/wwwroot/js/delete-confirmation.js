function showDeleteConfirmation(formElement, itemName) {
    if (formElement) {
        formElement.addEventListener('submit', function (e) {
            e.preventDefault();
            
            // Initial confirmation dialog
            Swal.fire({
                title: 'Delete Confirmation',
                html: `Are you sure you want to delete this ${itemName}?<br><br><small class="text-danger">This action cannot be undone.</small>`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Yes, delete it',
                cancelButtonText: 'Cancel',
                customClass: {
                    popup: 'delete-confirmation-popup'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    // Show loading state with custom message
                    Swal.fire({
                        title: 'Deleting...',
                        html: `<div class="delete-progress">
                                <p>Removing ${itemName} from system</p>
                                <div class="progress-bar"></div>
                              </div>`,
                        allowOutsideClick: false,
                        showConfirmButton: false,
                        didOpen: () => {
                            Swal.showLoading();
                            setTimeout(() => {
                                formElement.submit();
                            }, 800);
                        }
                    });
                }
            });
        });
    }
}

// Function to show error notification
function showNotification(type, message) {
    // Only show notification for errors
    if (type === 'error') {
        setTimeout(() => {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 4000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer);
                    toast.addEventListener('mouseleave', Swal.resumeTimer);
                }
            });

            Toast.fire({
                icon: 'error',
                title: message,
                customClass: {
                    popup: 'notification-popup'
                }
            });
        }, 300);
    }
}

// Add these styles to make the dialogs more attractive
const style = document.createElement('style');
style.textContent = `
    .delete-confirmation-popup {
        border-radius: 8px;
        padding: 20px;
    }

    .delete-progress {
        margin: 20px 0;
        text-align: center;
    }

    .progress-bar {
        height: 4px;
        width: 100%;
        background: #f3f3f3;
        border-radius: 2px;
        overflow: hidden;
        position: relative;
    }

    .progress-bar::after {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        height: 100%;
        width: 30%;
        background-color: #dc3545;
        animation: progress 1s ease-in-out infinite;
    }

    @keyframes progress {
        0% {
            left: -30%;
        }
        100% {
            left: 100%;
        }
    }

    .notification-popup {
        border-radius: 8px !important;
        font-size: 1rem !important;
        padding: 16px 24px !important;
        box-shadow: 0 4px 12px rgba(0,0,0,0.15) !important;
    }

    .notification-popup.swal2-toast {
        background: rgba(255, 255, 255, 0.95) !important;
        backdrop-filter: blur(10px);
    }

    .swal2-icon {
        margin: 1em auto 0.6em !important;
    }
`;
document.head.appendChild(style);
