document.querySelector('form').addEventListener('submit', async function (e) {
    e.preventDefault(); // Sayfanın yeniden yüklenmesini engelle
  
    // Form alanlarını al
    const nickname = document.getElementById('nickname').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
  
    // Şifre doğrulama
    if (password !== confirmPassword) {
      alert('Passwords do not match!');
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
        alert('Registration successful!');
        window.location.href = '/Home/Login'; // Başarılıysa login sayfasına yönlendir
      } else {
        const errorData = await response.json();
        alert(`Error: ${errorData.message || 'An error occurred!'}`);
      }
    } catch (error) {
      alert(`Error: ${error.message}`);
    }
});
