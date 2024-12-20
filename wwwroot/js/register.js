// Toast mesajını göstermek için bir fonksiyon
function showToast(message, type = 'info') {
  const toastContainer = document.getElementById('toast-container');
  const toast = document.createElement('div');
  toast.classList.add('toast', 'show', type); // 'success', 'error', 'info', 'warning'
  toast.textContent = message;

  // Toast mesajını ekle
  toastContainer.appendChild(toast);

  // Toast mesajını kaldırma (5 saniye sonra)
  setTimeout(() => {
      toast.classList.remove('show');
      setTimeout(() => toast.remove(), 500); // Animasyon sonrası öğeyi tamamen sil
  }, 5000);
}

// Form submit event
document.getElementById('registerForm').addEventListener('submit', async function (e) {
  e.preventDefault(); // Sayfanın yeniden yüklenmesini engelle

  // Form alanlarını al
  const nickname = document.getElementById('nickname').value.trim();
  const email = document.getElementById('email').value.trim();
  const password = document.getElementById('password').value;
  const confirmPassword = document.getElementById('confirmPassword').value;

  // Nickname boş olmamalı
  if (!nickname) {
      showToast('Nickname is required!', 'error');
      return;
  }

  // E-posta formatı kontrolü
  const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
  if (!emailPattern.test(email)) {
      showToast('Please enter a valid email address!', 'error');
      return;
  }

  // E-posta adresinin zaten kayıtlı olup olmadığını kontrol et
  const isEmailTaken = await checkEmailAvailability(email);
  if (isEmailTaken) {
      showToast('This email is already registered. Please use a different one.', 'error');
      return;
  }

  // Şifre güçlü olmalı: En az 8 karakter, bir büyük harf, bir rakam ve bir özel karakter
  const passwordStrengthPattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  if (!passwordStrengthPattern.test(password)) {
      showToast('Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.', 'error');
      return;
  }

  // Şifre doğrulama
  if (password !== confirmPassword) {
      showToast('Passwords do not match!', 'error');
      return;
  }

  // API'ye gönderilecek veriler
  const userData = {
      nickname: nickname,
      email: email,
      password: password,
  };

  try {
      // Fetch API ile POST isteği
      const response = await fetch('https://localhost:5001/api/Auth/register', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(userData),
      });

      // Yanıtı kontrol et
      if (response.ok) {
          showToast('Registration successful!', 'success');
          window.location.href = '/Home/Login'; // Başarılıysa login sayfasına yönlendir
      } else {
          const errorData = await response.json();
          // Eğer hata mesajı e-posta ile ilgiliyse, özel bir mesaj göster
          if (errorData.message.includes('Email is already in use')) {
              showToast('This email is already registered. Please use a different one.', 'error');
          } else {
              showToast(errorData.message || 'An error occurred!', 'error');
          }
      }
  } catch (error) {
      showToast(`Error: ${error.message}`, 'error');
  }
});

// E-posta adresinin kullanılabilirliğini kontrol et
async function checkEmailAvailability(email) {
  try {
      const response = await fetch('https://localhost:5001/api/Auth/check-email', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({ email: email }),
      });

      if (response.ok) {
          const data = await response.json();
          return data.isEmailTaken;  // true/false döndürür
      } else {
          showToast('An error occurred while checking the email.', 'error');
          return false;
      }
  } catch (error) {
      showToast(`Error: ${error.message}`, 'error');
      return false;
  }
}
