import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from "../../services/auth.service";

@Component({
    templateUrl: './invitation.page.html'
})
export class InvitationPage implements OnInit {
    showError = false;
    invitation: { from: string, timeLimit?: string };

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

        const result = await this.backendService.getInvitation(token);
        if (!result) {
            this.showError = true;
            this.authService.logout();
            return;
        }

        this.invitation = result;
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
