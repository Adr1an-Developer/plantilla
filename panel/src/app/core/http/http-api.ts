export class HttpApi {
  // OAuth
  static oauthLogin = 'api/Authorization/authenticate';

  // Start Mutual Auth
  static userRegister = 'api/user/register';

  // Regions
  static GetCountries = 'regions/countries/GetCountriesByCode';
  static GetStates = 'regions/countries/GetStatesByCountry';
  static GetCities = 'regions/countries/GetCitiesbByStateId';

  // User
  static UsersList = 'api/Users/getall';
  static getUser = 'api/Users/get';
  static addUser = 'api/Users/add';
  static updateUser = 'api/Users/update';
  static deleteUser = 'api/Users/delete';
  static changepassword = 'api/Users/changepassword';

  // Profile
  static profileList = 'api/Profile/getall';
  static getProfile = 'api/Profile/get';
  static addProfile = 'api/Profile/add';
  static updateProfile = 'api/Profile/update';
  static deleteProfile = 'api/Profile/delete';

  // Menu
  static MenuList = 'api/SystemMenu/getall';
  static getMenu = 'api/SystemMenu/get';
  static addMenu = 'api/SystemMenu/add';
  static updateMenu = 'api/SystemMenu/update';
  static deleteMenu = 'api/SystemMenu/delete';

  // MenuPermission
  static MenuPermissionList = 'api/MenuPermission/getall';
  static MenuPermissionListByProfile = 'api/MenuPermission/getbyprofile';
  static getMenuPermission = 'api/MenuPermission/get';
  static addMenuPermission = 'api/MenuPermission/add';
  static updateMenuPermission = 'api/MenuPermission/update';
  static deleteMenuPermission = 'api/MenuPermission/delete';
}
