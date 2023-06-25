import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { liveQuery } from 'dexie';
import { db, Item, TodoItem, TodoList } from '../../db/db';

@Component({
  selector: 'app-itemlist',
  templateUrl: './itemlist.component.html',
  styleUrls: ['./itemlist.component.css']
})
export class ItemlistComponent {
  @Input() todoList: TodoList;
  // Observe an arbritary query:
  todoItems$ = liveQuery(() => this.listTodoItems());

  async listTodoItems() {
    return db.todoItems
      .where({
        todoListId: this.todoList.id,
      })
      .toArray();
  }

  async addItem() {
    await db.todoItems.add(<TodoItem>{
      title: this.itemName,
      todoListId: this.todoList.id,
    });
  }

  itemName = 'My new item';
}
