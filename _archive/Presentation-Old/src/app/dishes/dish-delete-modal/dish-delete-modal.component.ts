import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DishService } from '../shared/dish.service';
import { Dish } from '../shared/dish.model';

@Component({
  selector: 'app-dish-delete-modal',
  templateUrl: './dish-delete-modal.component.html',
  styleUrls: ['./dish-delete-modal.component.css']
})
export class DishDeleteModalComponent implements OnInit {

  modalTitle: string;
  private dishId: number;
  private sub: Subscription;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private dishService: DishService,
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog) {
    this.modalTitle = data.title;
    this.dishId = data.id;
    console.log(data)
  }

  deleteDish(dishId): void {

    console.log(dishId);

    this.sub = this.route.params.subscribe(
      params => {
        this.dishId = params['dishId'];

        this.dishService.deleteDish(dishId)
          .subscribe(dish => {
            //this.dish = dish; // deze code is niet nodig..
            this.router.navigateByUrl('/dishes');
          })
      });
  }

  ngOnInit() {
  }
}
