import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from "../../services/auth.service";

@Component({
    templateUrl: './open-shared-link.page.html'
})
export class OpenSharedLinkPage implements OnInit {
    showError = false;

    constructor(private authService: AuthService, 
        private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit(): void {
        const token = this.route.snapshot.params["token"];
        if (!token) {
            this.showError = true;
            return;
        }

        this.authService.loginByToken(token).then(() => {
            this.backendService.getUserChapters().then((chapters) => {
                if (!chapters 
                    || chapters.length === 0 
                    || !chapters[0].userLessons 
                    || chapters[0].userLessons.length === 0) {
                    this.showError = true;
                } else {
                    const firstChapter = chapters[0];
                    this.router.navigate(['/task', firstChapter.id, firstChapter.userLessons[0].id]);
                }
            });
        }, () => this.showError = true);
    }
}
