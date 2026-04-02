// Toggle sidebar on mobile
document.querySelector('.sidebar-toggle')?.addEventListener('click', function (e) {
      e.preventDefault();
      document.querySelector('.sidebar')?.classList.toggle('show');
});

// Close sidebar when clicking outside on mobile
document.addEventListener('click', function (event) {
      const sidebar = document.querySelector('.sidebar');
      const toggle = document.querySelector('.sidebar-toggle');

      if (window.innerWidth <= 768 && sidebar?.classList.contains('show')) {
            if (!sidebar.contains(event.target) && !toggle?.contains(event.target)) {
                  sidebar.classList.remove('show');
            }
      }
});


// Show toast notifications
function showToast(message, type = 'success') {
      const toastContainer = document.querySelector('.toast-container');
      const toastId = 'toast-' + Date.now();

      const bgClass = type === 'success' ? 'bg-success' : (type === 'error' ? 'bg-danger' : 'bg-info');

      const toastHtml = `
                <div id="${toastId}" class="toast align-items-center text-white ${bgClass} border-0" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true" data-bs-delay="5000">
                    <div class="d-flex">
                        <div class="toast-body">
                            ${message}
                        </div>
                        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                    </div>
                </div>
            `;

      toastContainer.insertAdjacentHTML('beforeend', toastHtml);
      const toastElement = document.getElementById(toastId);
      const toast = new bootstrap.Toast(toastElement);
      toast.show();

      toastElement.addEventListener('hidden.bs.toast', function () {
            toastElement.remove();
      });
}

// Check for TempData messages
if (TempData["Success"] != null) {
      <text>showToast('@TempData["Success"]', 'success');</text>
}

if (TempData["Error"] != null) {
      <text>showToast('@TempData["Error"]', 'error');</text>
}

if (TempData["Info"] != null) {
      <text>showToast('@TempData["Info"]', 'info');</text>
}

// Confirm delete
function confirmDelete(message = 'هل أنت متأكد من الحذف؟') {
      return confirm(message);
}

// Format date
function formatDate(dateString) {
      const date = new Date(dateString);
      return date.toLocaleDateString('ar-EG');
}

// Format currency
function formatCurrency(amount) {
      return new Intl.NumberFormat('ar-EG', { style: 'currency', currency: 'EGP' }).format(amount);
}

function toggleModeal() {
      // إظهار أو إخفاء Modal
      var myModal = new bootstrap.Modal(document.getElementById('myModal'));
      myModal.show();
      myModal.hide();
}

function toggleToast() {
      // إظهار أو إخفاء Toast (الإشعارات)
      var toast = new bootstrap.Toast(document.getElementById('myToast'));
      toast.show();
}

function activateDropDown() {
      // تفعيل Dropdown
      var dropdown = new bootstrap.Dropdown(document.getElementById('dropdownButton'));
      dropdown.toggle();
}

function activateTooltip() {
      // تفعيل Tooltip
      var tooltip = new bootstrap.Tooltip(document.getElementById('tooltipElement'), {
            title: 'نص التوضيح',
            placement: 'top'
      });
}

function activateBigTooltip() {
      // تفعيل Popover (زي Tooltip لكن بحجم أكبر)
      var popover = new bootstrap.Popover(document.getElementById('popoverButton'), {
            content: 'المحتوى هنا',
            placement: 'right'
      });
}

function activateTabs() {
      // تفعيل Tab (للتبويبات)
      var tab = new bootstrap.Tab(document.getElementById('tabButton'));
      tab.show();
}

function activateCollapse() {
      // تفعيل Collapse (القوائم القابلة للطي)
      var collapse = new bootstrap.Collapse(document.getElementById('collapsePanel'), {
            toggle: true
      });
}
