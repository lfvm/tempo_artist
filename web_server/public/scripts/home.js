

const checkAuth = (e) => {
        //Si se encuentran los datos de un usuario autenticado ir a home, de lo contrario
        //ir a login
        const mail = localStorage.getItem('mail') ?? '';
        const password = localStorage.getItem('password') ?? '';
        
        if(mail !== '' && password !== ''){
            location.href = '/';
            
        }else{
            location.href = '/login';
            
        }
}