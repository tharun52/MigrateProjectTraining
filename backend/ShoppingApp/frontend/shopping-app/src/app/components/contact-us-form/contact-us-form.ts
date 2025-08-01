import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CaptchaService } from '../../services/captcha-service';
import { ContactService } from '../../services/contact-service';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

declare const grecaptcha: any;

@Component({
  selector: 'app-contact-us-form',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './contact-us-form.html',
  styleUrl: './contact-us-form.css'
})
export class ContactUsForm {
  contactForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private contactService: ContactService,
    private captchaService: CaptchaService,
    private router:Router
  ) {
    this.contactForm = new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', Validators.required),
      content: new FormControl('', Validators.required)
    });
  }

  handleContactSubmit() {
    if (this.contactForm.invalid) return;

    this.loading = true;
    this.error = null;

    grecaptcha.ready(() => {
      grecaptcha
        .execute(environment.captchakey, { action: 'submit' })
        .then((token: string) => {
          this.captchaService.validateCaptcha(token).subscribe({
            next: () => {
              const contactData = this.contactForm.value;

              this.contactService.submitContact(contactData).subscribe({
                next: () => {
                  this.loading = false;
                  alert('Your message has been submitted successfully!');
                  this.contactForm.reset();
                  this.router.navigateByUrl('home');
                },
                error: (err) => {
                  this.error = err.error?.message || 'Submission failed.';
                  this.loading = false;
                }
              });
            },
            error: () => {
              this.error = 'Captcha verification failed.';
              this.loading = false;
            }
          });
        });
    });
  }
}
