// class tokenLocalStorageHandler{
//     private token?:string;
//     constructor() {
//       this.token = localStorage.getItem('token') ?? '';
//     }
//     getToken():string {
//       return this.token;
//     }
//     setToken(token: string):void {
//       localStorage.setItem('token', token);
//       this.token = token;
//     }
//     isTokenSet():boolean
//     {
//       return this.token !== '';
//     }
//     getAuthorization():string
//     {
//       return 'Bearer ' + this.token;
//     }
//     removeToken():void
//     {
//       localStorage.setItem('token', '');
//       this.token = '';
//     }
// }


class tokenHandler{
    private readonly cookieName = 'Token';
    getToken():string | undefined {
      let matches = document.cookie.match(new RegExp(
        '(?:^|; )' + this.cookieName + '=([^;]*)'
      ));
      return matches ? decodeURIComponent(matches[1]) : undefined;

    }
    setToken(token: string, maxAge:number = 3600, options = {}) {
      options = {
        path: '/',
        ...options,
        'max-age': maxAge,
      };
    
      let updatedCookie = this.cookieName + '=' + encodeURIComponent(token);

      for (let optionKey in options) {
        updatedCookie += "; " + optionKey;
        let optionValue = options[optionKey];
        if (optionValue !== true) {
          updatedCookie += "=" + optionValue;
        }
      }
      
      document.cookie = updatedCookie;
    }
    isTokenSet():boolean
    {
      return this.getToken() !== undefined;
    }
    getAuthorization():{'Authorization': string}
    {
      if(this.isTokenSet())
      {
        return {'Authorization': 'Bearer ' + this.getToken()}
      } else {
        return;
      }
    }
    removeToken():void
    {
      let updatedCookie = this.cookieName + "=" + encodeURIComponent('token') + '; max-age=0';
      document.cookie = updatedCookie;
    }
  
}

export default tokenHandler;