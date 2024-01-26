import { BrowserRouter, Routes as Rotas, Route } from 'react-router-dom'
import Login from '../pages/loginAndRegister/Login'
import { LoadingProvider } from '../context/LoadingContext'
import Loading from '../components/loading/Loading'
import { AlertProvider } from '../context/PopupContext'
import ForgotPassword from '../pages/forgotPassword/ForgotPassword'
import PostLogin from '../pages/postLogin/PostLogin'
import Wrapper from './Wrapper'

const Routes = () => {

  return (
    <LoadingProvider>
      <AlertProvider>
        <BrowserRouter>
          <Loading />
          <Rotas>
            <Route
              path="/"
              element={
                <Wrapper isLoginPage={true}>
                  <Login />
                </Wrapper>
              }
            />
            <Route
              path="forgotPassword"
              element={<Wrapper><ForgotPassword /></Wrapper>}
            />
            <Route
              path="postLogin"
              element={<Wrapper><PostLogin /></Wrapper>}
            />
          </Rotas>
        </BrowserRouter>
      </AlertProvider>
    </LoadingProvider>
  )
}

export default Routes