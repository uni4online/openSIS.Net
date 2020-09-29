import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import icVisibility from '@iconify/icons-ic/twotone-visibility';
import icVisibilityOff from '@iconify/icons-ic/twotone-visibility-off';
import icLanguage from '@iconify/icons-ic/twotone-language';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { UserViewModel } from '../../../models/userModel';
import { LoginService } from '../../../services/login.service';
import { Observable, timer } from 'rxjs';
import { map } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
import { LoaderService } from '../../../services/loader.service';
import { ValidationService } from '../../shared/validation.service';


@Component({
  selector: 'vex-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    fadeInUp400ms
  ]
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  private date = new Date();
  time: Observable<Date> = timer(0, 1000).pipe(map(() => new Date()));
  inputType = 'password';
  visible = false;
  icVisibility = icVisibility;
  icVisibilityOff = icVisibilityOff;
  icLanguage = icLanguage;
  public tenant = "";
  UserModel: UserViewModel = new UserViewModel();
  loading: Boolean;

  constructor(
    private router: Router,
    private Activeroute: ActivatedRoute,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private snackbar: MatSnackBar,
    private loginService: LoginService,
    public translate: TranslateService,
    private loaderService: LoaderService
  ) {
    translate.addLangs(['en', 'fr']);
    translate.setDefaultLang('en');
    this.Activeroute.params.subscribe(params => { this.tenant = params.id || 'opensisv2'; });
    sessionStorage.setItem("tenant", this.tenant);
    this.loaderService.isLoading.subscribe((v) => {
      this.loading = v;
    });
  }
  get f() { return this.form.controls; }

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', [Validators.required, ValidationService.emailValidator]],
      password: ['', Validators.required]
    });
  }

  send() {
    if (this.form.dirty && this.form.valid) {

      this.UserModel._tenantName = this.tenant;
      this.loginService.ValidateLogin(this.UserModel).subscribe(data => {
        if (typeof (data) == 'undefined') {
          this.snackbar.open('Login failed. ' + sessionStorage.getItem("httpError"), '', {
            duration: 10000
          });
        }
        else {
          if (data._failure) {
            this.snackbar.open('Login failed. ' + data._message, 'LOL THANKS', {
              duration: 10000
            });
          } else {
            this.UserModel = data;
            sessionStorage.setItem("token", data._token);
            sessionStorage.setItem("tenantId", data.tenantId);
            sessionStorage.setItem("email", data.email);
            this.router.navigateByUrl("/school/dashboards");

          }
        }

      })

    }

  }

  toggleVisibility() {
    if (this.visible) {
      this.inputType = 'password';
      this.visible = false;
      this.cd.markForCheck();
    } else {
      this.inputType = 'text';
      this.visible = true;
      this.cd.markForCheck();
    }
  }

  switchLang(lang: string) {
    this.translate.use(lang);
  }

}
