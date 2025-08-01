import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { AuthGuard } from './guards/auth.guard';
import { NoAuthGuard } from './guards/no-auth.guard';
import { Login } from './components/login/login';
import { Signup } from './components/signup/signup';
import { AdminDashboard } from './components/admin-dashboard/admin-dashboard';
import { Welcome } from './components/welcome/welcome';
import { SignUpAdmin } from './components/sign-up-admin/sign-up-admin';
import { Cart } from './components/cart/cart';
import { PlaceOrder } from './components/place-order/place-order';
import { ViewOrders} from './components/view-orders/view-orders';
import { NewsList } from './components/news-list/news-list';
import { ContactUsForm } from './components/contact-us-form/contact-us-form';

export const routes: Routes = [
  { path: 'home', component: Home, canActivate: [AuthGuard], data:{role:'User'} },
  { path: 'cart', component: Cart, canActivate: [AuthGuard], data:{role:'User'}  },
  { path: 'news', component: NewsList, canActivate: [AuthGuard], data:{role:'User'}  },
  { path: 'contact', component: ContactUsForm, canActivate: [AuthGuard], data:{role:'User'}  },
  { path: 'view-orders', component: ViewOrders, canActivate: [AuthGuard], data:{role:'User'}  },
  { path: 'place-order', component: PlaceOrder, canActivate: [AuthGuard], data:{role:'User'}  },
  { path: 'admin', component: AdminDashboard, canActivate: [AuthGuard], data: { role: 'Admin' }},
  { path: 'login', component: Login, canActivate: [NoAuthGuard] },
  { path: 'signup', component: Signup, canActivate: [NoAuthGuard] },
  { path: 'signup/admin', component: SignUpAdmin, canActivate: [NoAuthGuard] },
  { path: '', component: Welcome, canActivate: [NoAuthGuard] },
];
