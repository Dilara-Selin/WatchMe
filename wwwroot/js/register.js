document.getElementById('registerForm').addEventListener('submit', async function (e) {
    e.preventDefault();
  
    const nickname = document.getElementById('nickname').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
  
    if (!nickname) {
      showToast('Nickname is required!', 'error');
      return;
    }
  
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailPattern.test(email)) {
      showToast('Please enter a valid email address!', 'error');
      return;
    }
  
    const isEmailTaken = await checkEmailAvailability(email);
    if (isEmailTaken) {
      showToast('This email is already registered. Please use a different one.', 'error');
      return;
    }
  
    const passwordStrengthPattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if (!passwordStrengthPattern.test(password)) {
      showToast('Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.', 'error');
      return;
    }
  
    if (password !== confirmPassword) {
      showToast('Passwords do not match!', 'error');
      return;
    }
  
    const userData = { nickname, email, password };
  
    try {
      const response = await fetch('https://localhost:5001/api/Auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(userData)
      });
  
      if (response.ok) {
        showToast('Registration successful!', 'success');
        window.location.href = '/Home/Login';
      } else {
        const errorData = await response.json();
        showToast(errorData.message || 'An error occurred!', 'error');
      }
    } catch (error) {
      showToast(`Error: ${error.message}`, 'error');
    }
  });
  