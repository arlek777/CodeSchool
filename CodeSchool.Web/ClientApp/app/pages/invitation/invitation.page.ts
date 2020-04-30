import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from "../../services/auth.service";

@Component({
    templateUrl: './invitation.page.html'
})
export class InvitationPage implements OnInit {
    showError = false;

    constructor(private authService: AuthService, 
        private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router) {
    }

    async ngOnInit() {
        const token = this.route.snapshot.params["token"];
        if (!token) {
            this.showError = true;
            return;
        }

        const valid = await this.backendService.verifyInvitationToken(token);
        if (!valid) {
            this.showError = true;
            this.authService.logout();
        }
    }

    start() {
        const token = this.route.snapshot.params["token"];

        this.authService.loginByToken(token).then(() => {
            this.backendService.getFirstChapterAndLesson().then((data: any) => {
                if (!data) {
                    this.showError = true;
                } else {
                    this.router.navigate(['/task', data.userChapterId, data.userLessonId]);
                }
            });
        }, () => this.showError = true);
    }
}
