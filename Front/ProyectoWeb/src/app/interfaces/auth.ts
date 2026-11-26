export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest{
  email: string;
  password: string;
  name: string;
  description: string;
  imageUrl: string;
}

export interface UpdateAccountRequest {
  name: string;
  description: string;
  imageUrl: string;
  password: string;
}

/** Lo que tiene el formulario de registro: igual al RegisterRequest, más la contraseña repetida (que no se manda al back) */
export interface RegisterFormModel extends RegisterRequest {
  password2: string;
}
